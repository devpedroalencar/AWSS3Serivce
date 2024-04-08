using Amazon.S3.Model;
using Amazon.S3;
using AWSS3Service.Domain;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;

namespace AWSS3Service.Repositories
{
    internal class s3Repository
    {
        private readonly IAmazonS3? S3Client;
        private readonly string _bucketName = "bucket";
        private readonly string _prefixo;

        public s3Repository(Prefix prefix)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;
            if (chain.TryGetAWSCredentials("default", out awsCredentials))
            {
                S3Client = new AmazonS3Client(awsCredentials);
            }
            _prefixo = prefix.getPrefix();

            //var credentials = new Amazon.Runtime.BasicAWSCredentials("", "");
            //S3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
        }

        public async Task<List<S3Object>> getS3ObjectList()
        {
            ListObjectsV2Request response = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = _prefixo
            };

            ListObjectsV2Response listObj = await S3Client.ListObjectsV2Async(response);
            return listObj.S3Objects;
        }

        public async Task<List<GetObjectResponse>> getOjectAsStream()
        {
            try
            {
                List<GetObjectResponse> responseList = new List<GetObjectResponse>();
                var obj = await getS3ObjectList();

                foreach (var itemObj in obj)
                {
                    GetObjectResponse response = await S3Client.GetObjectAsync(_bucketName, itemObj.Key);
                    responseList.Add(response);
                }
                return responseList;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Cheque suas cridenciais do AWS.");
                }
                else
                {
                    throw new Exception("Ocorreu um erro: " + amazonS3Exception.Message);
                }
            }
        }

        public async void UploadFileBucket(Stream fileStream, string key, string contentType)
        {
            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = fileStream
                };

                if (contentType != null)
                {
                    putRequest.ContentType = contentType;
                }

                await S3Client.PutObjectAsync(putRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Cheque suas cridenciais do AWS.");
                }
                else
                {
                    throw new Exception("Ocorreu um erro: " + amazonS3Exception.Message);
                }
            }
        }

        public async Task DeleteFileBucket()
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = _prefixo
                };

                await S3Client.DeleteObjectAsync(deleteObjectRequest);
            }
            catch (AmazonS3Exception e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task DeleteDiretorioBucket()
        {
            try
            {
                var listObjectsRequest = new ListObjectsRequest
                {
                    BucketName = _bucketName,
                    Prefix = _prefixo
                };

                var listObjectsResponse = await S3Client.ListObjectsAsync(listObjectsRequest);

                foreach (var s3Object in listObjectsResponse.S3Objects)
                {
                    var deleteObjectRequest = new DeleteObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = s3Object.Key
                    };

                    //await S3Client.DeleteObjectAsync(deleteObjectRequest);
                }
            }
            catch (AmazonS3Exception e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
