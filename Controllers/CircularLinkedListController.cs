using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CircularLinkedListController : ControllerBase
    {
        private readonly CircularLinkedList _list;
        public CircularLinkedListController(CircularLinkedList list) => _list = list;

        // GET: /api/circularlinkedlist
        [HttpGet]
        public IActionResult Traverse()
        {
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Circular list has {_list.Size} node(s)."));
        }

        // POST: /api/circularlinkedlist/insert-beginning
        [HttpPost("insert-beginning")]
        public IActionResult InsertBeginning([FromBody] ValueRequest req)
        {
            _list.InsertAtBeginning(req.Value);
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Inserted {req.Value} at beginning."));
        }

        // POST: /api/circularlinkedlist/insert-end
        [HttpPost("insert-end")]
        public IActionResult InsertEnd([FromBody] ValueRequest req)
        {
            _list.InsertAtEnd(req.Value);
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Inserted {req.Value} at end."));
        }

        // DELETE: /api/circularlinkedlist/delete-beginning
        [HttpDelete("delete-beginning")]
        public IActionResult DeleteBeginning()
        {
            var (ok, msg) = _list.DeleteFromBeginning();
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // DELETE: /api/circularlinkedlist/delete-end
        [HttpDelete("delete-end")]
        public IActionResult DeleteEnd()
        {
            var (ok, msg) = _list.DeleteFromEnd();
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // POST: /api/circularlinkedlist/search
        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchRequest req)
        {
            int idx = _list.Search(req.Key);
            if (idx == -1)
                return NotFound(ApiResponse<int>.Fail($"{req.Key} not found."));
            return Ok(ApiResponse<int>.Ok(idx, $"{req.Key} found at index {idx}."));
        }

        // POST: /api/circularlinkedlist/reset
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _list.Clear();
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), "List cleared."));
        }
                // ⭐ NEW ENDPOINT: POST /api/circularlinkedlist/insert-position
        [HttpPost("insert-position")]
        public IActionResult InsertPosition([FromBody] InsertPositionRequest req)
        {
            var (ok, msg) = _list.InsertAtPosition(req.Position, req.Value);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // ⭐ NEW ENDPOINT: DELETE /api/circularlinkedlist/delete-position
        [HttpDelete("delete-position")]
        public IActionResult DeletePosition([FromBody] DeletePositionRequest req)
        {
            var (ok, msg) = _list.DeleteAtPosition(req.Position);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // ⭐ NEW ENDPOINT: DELETE /api/circularlinkedlist/delete-value
        [HttpDelete("delete-value")]
        public IActionResult DeleteValue([FromBody] DeleteValueRequest req)
        {
            var (ok, msg) = _list.DeleteByValue(req.Value);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // ⭐ NEW ENDPOINT: PUT /api/circularlinkedlist/update
        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateRequest req)
        {
            var (ok, msg) = _list.UpdateAtPosition(req.Position, req.NewValue);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }
    }
}