using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/todo")]
    public class TodoController : Controller
    {
        [FromServices]
        public ITodoRepository TodoItems { get; set; }

        // Returns 200 with JSON Response Body
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return new ObjectResult(item);
        }

        // Returns 201 with JSON Response Body
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return HttpBadRequest();
            }

            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { Controller = "Todo", id = item.Key }, item);
        }

        // Returns 204 (No Content) Response
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return HttpBadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }

            TodoItems.Update(item);
            return new NoContentResult();
        }

        // Returns 204 (No Content) Response
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TodoItems.Remove(id);
        }

    }
}
