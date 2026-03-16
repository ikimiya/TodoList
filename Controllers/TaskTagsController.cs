using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskTagsController : ControllerBase
{
    private readonly TaskTags _taskTags;
    public TaskTagsController(TaskTags taskTags)
    {
        _taskTags = taskTags;
    }


}
