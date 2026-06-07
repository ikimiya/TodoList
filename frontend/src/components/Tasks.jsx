function Tasks()
{

    function logout()
    {
        localStorage.removeItem('token');
        window.location.reload();
    }

    function createCategory()
    {
        console.log("createCategory");
    }

    function createTask()
    {
        console.log("createTask");
    }

    function showTab(tab)
    {
        console.log(tab);
    }

    function taskPage()
    {
        return (
    
          <>
          {/* Navbar */}
        < nav className = "navbar navbar-dark bg-dark mb-4" >
          < div className = "container" >
            < span className = "navbar-brand" >📝 TodoList </ span >

            < button
                onClick ={ logout}
        className = "btn btn-outline-light btn-sm"
      >
        Logout
      </ button >

    </ div >

  </ nav >


  < div className = "container" >
  
          {/* Create Category */}
          < div className = "card shadow mb-4" >
            < div className = "card-body" >
              < h5 className = "card-title" > Create Category </ h5 >

              < div className = "row g-2" >
                < div className = "col-md-4" >
                  < input
                      type = "text"
                      id = "categoryName"
                      className = "form-control"
                      placeholder = "Category name"
                  />
                </ div >

                < div className = "col-md-2" >
                  < button onClick ={ createCategory}
        className = "btn btn-success w-100" >
                    Add Category
                  </ button >
                </ div >
              </ div >
            </ div >
          </ div >
  
          {/* Create Task */}
          < div className = "card shadow mb-4" >
            < div className = "card-body" >
              < h5 className = "card-title" > Create New Task</ h5 >

              < div className = "row g-2" >
                < div className = "col-md-3" >
                  < input id = "taskTitle" className = "form-control" />
                </ div >

                < div className = "col-md-3" >
                  < input id = "taskDescription" className = "form-control" />
                </ div >

                < div className = "col-md-2" >
                  < select id = "taskPriority" className = "form-select" >
                    < option value = "0" > Low </ option >
                    < option value = "1" > Medium </ option >
                    < option value = "2" > High </ option >
                  </ select >
                </ div >

                < div className = "col-md-2" >
                  < select id = "taskCategory" className = "form-select" >
                    < option value = "" > Select Category </ option >
                  </ select >
                </ div >

                < div className = "col-md-2" >
                  < button onClick ={ createTask}
        className = "btn btn-primary w-100" >
                    Add Task
                  </ button >
                </ div >
              </ div >
            </ div >
          </ div >
  
          {/* Tabs */}
          < ul className = "nav nav-tabs mb-3" >
            < li className = "nav-item" >
              < button
                  className = "nav-link active"
                  onClick ={ () => showTab('active')}
              >
                Active Tasks
              </ button >
            </ li >

            < li className = "nav-item" >
              < button
                  className = "nav-link"
                  onClick ={ () => showTab('deleted')}
              >
                Deleted Tasks
              </ button >
            </ li >
          </ ul >
  
          {/* Tasks */}
          < div className = "card shadow" >
            < div className = "card-body" >
              < div id = "tasksList" ></ div >
            </ div >
          </ div >

          < div className = "card shadow d-none" >
            < div className = "card-body" >
              < div id = "deletedList" ></ div >
            </ div >
          </ div >

        </ div >
      </>
      );
    }

    return taskPage();
}

export default Tasks;