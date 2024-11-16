using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using Amazon.S3.Model;
using ModelList;

namespace GradeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string DefaultBucketName = "mmojica";

        public BucketController(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string? bucketName = null, string? prefix = null)
        {
            bucketName ??= DefaultBucketName;

            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(request);
            return Ok($"File {prefix}/{file.FileName} uploaded to S3 bucket {bucketName} successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilesAsync(string? bucketName = null, string? prefix = null)
        {
            bucketName ??= DefaultBucketName;

            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };

            var result = await _s3Client.ListObjectsV2Async(request);

            var credentials = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };

                return new Credential()
                {
                    StudentName = s.Key,
                    CourseSection = "Generated URL",
                    Average = 0
                };
            });

            return Ok(credentials);
        }
    }
}
