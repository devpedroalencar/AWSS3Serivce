using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using AWSS3Service.Enums;
using AWSS3Service.Models.Requests;
using AWSS3Service.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSS3Service;

public class Function
{
    //public Function()
    //{
    //}

    /// <summary>
    /// Esse metodo � a entrada do servi�o, recebe um inteiro onde coverte para um Enum de invoca��o
    /// to respond to S3 notifications.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <param name="request"> classe de requisi��o, o eTypeCallInt � um inteiro que informa qual meotodo chamar. ele � convertido
    /// em eTypeCall </param>
    /// <returns></returns>
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context, ServiceRequest request)
    {
        try
        {
            var imageService = new ImageService(request);

            switch (request.eTypeCall)
            {
                case EInvoke.ImageOSSave:
                    await imageService.saveFile(request);
                    break;
                default:
                    break;
            }


        }
        catch (Exception)
        {

            throw;
        }
    }
}