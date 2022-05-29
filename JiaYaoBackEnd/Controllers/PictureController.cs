using JiaYao.Authorization;
using JiaYao.OSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        [MyNoAuthentication]
        public async Task<ActionResult<PicUploadResult>> UploadAsync([FromForm] FileReportDto fileModel)
        {
            //需要存储文件
            PicUploadResult result = PicUploadBll.AsyncPutObject(fileModel.File.OpenReadStream(), fileModel.File.FileName);
            if (result.status == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        public class FileReportDto
        {
            public IFormFile File { get; set; }
        }


    }
}
