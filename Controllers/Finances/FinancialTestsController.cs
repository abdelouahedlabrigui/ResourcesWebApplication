using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Finance;

namespace ResourcesWebApplication.Controllers.Finances
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialTestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinancialTestsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetTestNames")]
        public async Task<IActionResult> GetTestNames(){
            try
            {
                var test_names = await _context.TestResults.Select(s => new TestName {Name = s.TestName}).Distinct().ToListAsync();
                if (test_names.Count == 0)
                {
                    return NotFound();
                }
                return Ok(test_names);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTestNames")]
        public async Task<IActionResult> GetTestResultByName(string test_name){
            try
            {
                var test_result = await _context.TestResults.Where(w => w.TestName == test_name).ToListAsync();
                if (test_result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(test_result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        
        [HttpGet]
        [Route("DeleteTestResultByTestName")]
        public async Task<IActionResult> DeleteTestResult(string test_name) {
            try
            {
                if (string.IsNullOrWhiteSpace(test_name.ToString()))
                {
                    return NotFound();
                }
                var find = await _context.TestResults.Where(w => w.TestName == test_name).ToListAsync();
                _context.TestResults.RemoveRange(find);
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Delete test: {test_name}."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetTestResults")]
        public async Task<IActionResult> GetTestResults()
        {
            try
            {                
                var testResult = await _context.TestResults.OrderByDescending(d => d.Id).ToListAsync();
                if (testResult.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(testResult);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("POSTTestResults")]
        public async Task<IActionResult> POSTTestResults(string testName, double expectedValue, double actualValue, bool isSuccess, string errorMessage)
        {
            try
            {
                TestResult testResult = new TestResult
                {
                    TestName = testName,
                    ExpectedValue = expectedValue,
                    ActualValue = actualValue,
                    IsSuccess = isSuccess,
                    ErrorMessage = errorMessage,
                };

                if (testResult == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.TestResults.Add(testResult);
                    await _context.SaveChangesAsync();                        
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}