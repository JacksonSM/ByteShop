using ByteShop.Application.UseCases.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ByteShop.API.Tools
{
    public class ParseRequestResult<T> : Controller
    {
        public ActionResult ParseToActionResult(RequestResult<T> request, HttpResponse response = null)
        {
            switch (request.StatusCode)
            {
                case (int)HttpStatusCode.OK:
                    if (request.Header is not null && response is not null)
                        response.Headers.Add(request.Header.Item1, request.Header.Item2);
                    return Ok(request);
                case (int)HttpStatusCode.Created:
                    return Created(string.Empty, request);
                case (int)HttpStatusCode.NoContent:
                    return NoContent();
                case (int)HttpStatusCode.NotFound:
                    return NotFound(request);
                case (int)HttpStatusCode.BadRequest:
                    return BadRequest(request);
                default:
                    return BadRequest(request);
            }
        }
    }
}