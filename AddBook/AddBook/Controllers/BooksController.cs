using AddBook.Models;
using AddBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AddBook.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetBookbyName")]
         public ActionResult Getbooks(string name)
        {
            Booksop bo = new Booksop();
            Books b = new Books();
            b =bo.Viewbook(name);
            return Ok(b);
        }
        [HttpGet("GetAllBooks")]
        public ActionResult GetAllbooks()
        {
            Booksop bo = new Booksop();
            List<Books> li = new List<Books>();
            li = bo.viewAllBooks();
            return Ok(li);
        }
        [HttpPost("AddABook")]
        public ActionResult InsertRecords(Books b)
        {
            Booksop bo = new Booksop();
            string res = bo.addBook(b);
            if(res=="success")
            {
                return Ok(b);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet("DelistedBooks")]
        public ActionResult ViewDelistedBooks()
        {
            Booksop bo = new Booksop();
            List<Books> li = new List<Books>();
            li = bo.DelistedBooks();
            return Ok(li);
        }
        [HttpDelete("Deletebyname")]
        public ActionResult Delete(string name)
        {
            Booksop bo = new Booksop();
            string res = bo.DeleteBook(name);
            if (res == "success")
            {
                return Ok("Successfully Deleted");
            }
            else
            {
                return BadRequest("Try again");
            }

        }

        [HttpPut("DelistByName")]
        public ActionResult Delist(string name)
        {
            Booksop bo = new Booksop();
            string res = bo.DelistBook(name);
            if (res == "success")
            {
                return Ok("Successfully Delisted");
            }
            else
            {
                return BadRequest("Try again");
            }

        }

        [HttpPut("EnlistByName")]
        public ActionResult Enlist(string name)
        {
            Booksop bo = new Booksop();
            string res = bo.EnlistBook(name);
            if (res == "success")
            {
                return Ok("Successfully Enlisted");
            }
            else
            {
                return BadRequest("Try again");
            }

        }
        [HttpPut("EditBookByName")]
        public ActionResult Put(string name, [FromBody] Books b)
        {
            Booksop bs = new Booksop();
            string res = bs.EditBookDetails(name, b);
            if (res == "success")
            {
                return Ok(b);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
