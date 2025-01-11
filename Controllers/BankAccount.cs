using Dapper;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;

namespace delteaching.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccount: ControllerBase
    {
        private readonly DapperContext _context;
        public BankAccount(DapperContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(BankAccountDTO body)
        {
            var sql =
                @"INSERT INTO DelTeaching.dbo.BankAccounts(branch, number, [type], holderName, holderEmail, holderDocument, holderType, createdAt, updatedAt)" +
                " VALUES(@branch, @number, @type, @holderName, @holderEmail, @holderDocument, 0, getutcdate(), NULL)";

            using ( var connection = _context.CreateConnection())
            {
                try
                {
                    var rowsAffected = connection.Execute(sql,
                        new {branch = body.branch, number = body.number, type = body.type, holderName = body.holderName, holderEmail = body.holderEmail, holderDocument = body.holderDocument, });
                    if(rowsAffected > 0)
                    {
                        return Created();
                    }else
                    {
                        return BadRequest();
                    }
                }catch (Exception ex)
                {
                    var entity = new JsonResult("Erro inesperado: " + ex.Message)
                    {
                        StatusCode = 500
                    };
                    return entity;
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id) {

            var sql = @"SELECT * FROM DelTeaching.dbo.BankAccounts where id=@id;";

            using ( var connection = _context.CreateConnection())
            {
                try
                {
                    var bankAccount = connection.Query(sql, new { id = id });
                    if (bankAccount == null || bankAccount.Count() < 1)
                    {
                        return BadRequest("Conta não encontrada");
                    }
                    return Ok(bankAccount);
                }
                catch (Exception ex)
                {
                    var entity = new JsonResult("Erro inesperado. Estamos trabalhando para resolver isso em alguns instantes!" + ex.Message)
                    {
                        StatusCode = 500
                    };
                    return entity;
                }
            }
        }
    }
}
