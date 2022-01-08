using Core.DTO;
using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using TinCan;
using TinCan.LRSResponses;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XApiController : ControllerBase
    {
         readonly IXApi apiService;

        public XApiController(IXApi apiService)
        {
            this.apiService = apiService;
        }


        [HttpGet]
        [Route("GetStatement")]
        public List<Statement> getStatement()
        {
            return apiService.GetStatements();
        }


        [HttpPost]
        [Route("SendStatement")]
        public bool sendStatement()
        {
            return apiService.SendStatement();    
        }

        [ProducesResponseType(typeof(List<TaskResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TaskResult), StatusCodes.Status400BadRequest)]
        [Route("UploadScorm")]
        [HttpPost]
        public TaskResult UploadScorm()
        {
            string myPath = "C:\\Users\\User\\source\\repos\\WebApplication2\\Core\\ScormFiles\\";
            TaskResult result = new TaskResult();
            try
            {
                var file = Request.Form.Files[0];
                byte[] fileContent;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileContent = ms.ToArray();
                }
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                //decoder for image name , no duplicate errors
                string attachmentFileName = $"{fileName}.{Path.GetExtension(file.FileName).Replace(".", "")}";
                //path for angualr project file C:\\Users\\User\\source\\repos\\WebApplication2\\Core\\ScormFiles\\
                var fullPath = Path.Combine(myPath, attachmentFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

               // string startPath = @"c:\example\start";
                string zipPath = Path.Combine(myPath, attachmentFileName);
                var extractPath = Path.Combine(@"C:\\Users\\User\\source\\repos\\WebApplication2\\Core\\Extracted Scorm FIles\\", attachmentFileName);

              

                ZipFile.ExtractToDirectory(zipPath, extractPath);

                result.result = true;
                result.description = "Task Completed Succesfully";
                return result;
            }
            catch (Exception e)
            {
                result.result = false;
                result.description = e.Message;
                return result;
            }
        }


    }
 
}
