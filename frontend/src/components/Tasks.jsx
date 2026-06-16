import { useEffect, useState } from "react";

function Tasks()
{
    const [tasks, setTasks] = useState([]);
    const [deletedTasks, setDeletedTasks] = useState([]);
    const [activeTab, setActiveTab] = useState('active');
    const [categories, setCategories] = useState([]);
    const token = localStorage.getItem('token');

    const headers = {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
    };
    function logout()
    {
        localStorage.removeItem('token');
        window.location.reload();
    }

    function showTab(tab) {
        setActiveTab(tab);
        if (tab === 'active') {
            loadTasks();
        } else {
            loadDeletedTasks();
        }
    }

    {/* GET */ }
    const loadCategories = async () => {
        try {
            const response = await fetch('/Categories', { headers });
            const categories = await response.json();
            setCategories(categories);
            const select = document.getElementById('taskCategory');
            select.innerHTML = '<option value="">Select Category</option>';
            categories.forEach(cat => {
                select.innerHTML += `<option value="${cat.id}">${cat.name}</option>`;
            });
        } catch (err) {
            console.error("Error fetching categories:", err);
        }
    };

    const loadTasks = async () => {
        try {
            const response = await fetch('/Tasks', { headers });
            const taskData = await response.json();
            setTasks(taskData);

        } catch (err) {
            console.error("Error fetching tasks:", err);
        }
    };

    const loadDeletedTasks = async () => {
        try {
            const response = await fetch('/Tasks/deleted', { headers });
            const taskData = await response.json();
            setDeletedTasks(taskData);
        } catch (err) {
            console.error("Error fetching deleted tasks:", err);
        }
    };

    { /* POST */ }
    const createCategory = async () => {
        const name = document.getElementById('categoryName').value;
        if (!name) { alert("Category name cannot be empty"); return; }
        const response = await (await fetch('/Categories', {
            method: 'POST',
            headers,
            body: JSON.stringify({ name })

        }))
        if (response.ok) {
            document.getElementById('categoryName').value = '';
            await loadCategories();
            console.log("Created Category");

        } else {
            const errorData = await response.json();
            alert(errorData.message || "Error creating category");
        }
    }

    const createTask = async () => {
        const title = document.getElementById('taskTitle').value;
        const description = document.getElementById('taskDescription').value;
        const priority = parseInt(document.getElementById('taskPriority').value);
        const categoryId = parseInt(document.getElementById('taskCategory').value);

        if (!title) { alert('Please enter a task title'); return; }
        if (!categoryId) { alert('Please select a category'); return; }

        const response = await fetch('/Tasks', {
            method: 'POST',
            headers,
            body: JSON.stringify({ title, description, priority, categoryId, status: 0 })
        });

        if (response.ok) {
            document.getElementById('taskTitle').value = '';
            document.getElementById('taskDescription').value = '';
            await loadTasks();
        } else {
            alert('Failed to create task');
        }
    }

    { /*Delete*/ }
    const deleteTask = async (id) => {
        if (!confirm('Delete this task?')) return;

        const response = await fetch(`/Tasks/${id}`, {
            method: 'DELETE',
            headers
        });

        if (response.ok) await loadTasks();
    }

    function priorityLabel(p) { return ['Low', 'Medium', 'High'][p] || 'Low'; }
    function priorityBadge(p) { return ['bg-success', 'bg-warning', 'bg-danger'][p] || 'bg-success'; }
    function statusLabel(s) { return ['Pending', 'In Progress', 'Completed'][s] || 'Pending'; }
    function statusBadge(s) { return ['bg-secondary', 'bg-primary', 'bg-success'][s] || 'bg-secondary'; }


    useEffect(() => {
        if (!token) {
            window.location.replace('/index.html');
        }
        async function init() {
            await loadCategories();
            await loadTasks();
            await loadDeletedTasks();
        }
        init();
    },[]);

    function taskPage()
    {
        return (
            <>
                {/* Navbar */}
                < nav className="navbar navbar-dark bg-dark mb-4" >
                    < div className="container" >
                        < span className="navbar-brand" >📝 TodoList </ span >
                        < button onClick={logout}
                            className="btn btn-outline-light btn-sm">
                            Logout
                        </ button >
                    </ div >
                </ nav >

                < div className="container" >
                    {/* Create Category */}
                    < div className="card shadow mb-4" >
                        < div className="card-body" >
                            < h5 className="card-title" > Create Category </ h5 >

                            < div className="row g-2" >
                                < div className="col-md-4" >
                                    < input
                                        type="text"
                                        id="categoryName"
                                        className="form-control"
                                        placeholder="Category name"
                                    />
                                </ div >

                                < div className="col-md-2" >
                                    < button onClick={createCategory}
                                        className="btn btn-success w-100" >
                                        Add Category
                                    </ button >
                                </ div >
                            </ div >
                        </ div >
                    </ div >

                    {/* Create Task */}
                    < div className="card shadow mb-4" >
                        < div className="card-body" >
                            < h5 className="card-title" > Create New Task</ h5 >

                            < div className="row g-2" >
                                < div className="col-md-3" >
                                    <input id="taskTitle" className="form-control" placeholder="Task title" />
                                </ div >

                                < div className="col-md-3" >
                                    <input id="taskDescription" className="form-control" placeholder="Description" />
                                </ div >

                                < div className="col-md-2" >
                                    < select id="taskPriority" className="form-select" >
                                        < option value="0" > Low </ option >
                                        < option value="1" > Medium </ option >
                                        < option value="2" > High </ option >
                                    </ select >
                                </ div >

                                < div className="col-md-2" >
                                    < select id="taskCategory" className="form-select" >
                                        < option value="" > Select Category </ option >
                                    </ select >
                                </ div >

                                < div className="col-md-2" >
                                    < button onClick={createTask}
                                        className="btn btn-primary w-100" >
                                        Add Task
                                    </ button >
                                </ div >
                            </ div >
                        </ div >
                    </ div >

                    {/* Tabs */}
                    <ul className="nav nav-tabs mb-3">
                        <li className="nav-item">
                            <a className={`nav-link ${activeTab === 'active' ? 'active' : ''}`} onClick={() => showTab('active')}>
                                Active Tasks
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className={`nav-link ${activeTab === 'deleted' ? 'active' : ''}`} onClick={() => showTab('deleted')}>
                                Deleted Tasks
                            </a>
                        </li>
                    </ul>

                    {/* Active Tasks */}
                    {activeTab === 'active' && (
                        <div className="card shadow">
                            <div className="card-body">
                                {tasks.length === 0 && <p className="text-muted">No tasks yet. Create one above!</p>}
                                {tasks.map(task => (
                                    <div className="d-flex justify-content-between align-items-center border-bottom py-2" key={task.id}>
                                        <div>
                                            <strong>{task.title}</strong>
                                            <span className={`badge ${priorityBadge(task.priority)} ms-2`}>{priorityLabel(task.priority)}</span>
                                            <span className={`badge ${statusBadge(task.status)} ms-2`}>{statusLabel(task.status)}</span>
                                            <span className="badge border border-primary text-primary ms-2">{categories.find(c => c.id === task.categoryId)?.name || 'No Category'}</span>
                                            <p className="text-muted mb-0 small">{task.description || ''}</p>
                                        </div>
                                        <button onClick={() => deleteTask(task.id)} className="btn btn-danger btn-sm">Delete</button>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}

                    {/* Deleted Tasks */}
                    {activeTab === 'deleted' && (
                        <div className="card shadow">
                            <div className="card-body">
                                {deletedTasks.length === 0 && <p className="text-muted">No deleted tasks.</p>}
                                {deletedTasks.map(task => (
                                    <div className="d-flex justify-content-between align-items-center border-bottom py-2" key={task.id}>
                                        <div>
                                            <strong>{task.title}</strong>
                                            <span className="badge bg-secondary ms-2">Deleted</span>
                                            <span className="badge border border-primary text-primary ms-2">{categories.find(c => c.id === task.categoryId)?.name || 'No Category'}</span>
                                            <p className="text-muted mb-0 small">Deleted at: {new Date(task.deletedAt).toLocaleDateString()}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}

                </div>
      </>
      );
    }
    return taskPage();
}

export default Tasks;