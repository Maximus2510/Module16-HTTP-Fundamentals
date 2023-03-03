using System;
using System.Net;
using System.Threading;

namespace Module16_HTTP_Fundamentals_HTTP_Listener
{
    class Listener
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add("http://localhost:8888/");

            listener.Prefixes.Add("http://localhost:8888/Information/");
            listener.Prefixes.Add("http://localhost:8888/Success/");
            listener.Prefixes.Add("http://localhost:8888/Redirection/");
            listener.Prefixes.Add("http://localhost:8888/ClientError/");
            listener.Prefixes.Add("http://localhost:8888/ServerError/");

            listener.Start();

            Console.WriteLine("Listening...");

            Thread requestHandlerThread = new Thread(() =>
            {
                while (true)
                {

                    HttpListenerContext context = listener.GetContext();

                    string resourcePath = context.Request.Url.LocalPath.Trim('/');

                    Console.WriteLine("\n");
                    Console.WriteLine("Request received: " + resourcePath);

                    if (resourcePath == "MyName")
                    {
                        string responseString = GetMyName();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                    else if (resourcePath == "Information")
                    {
                        string responseString = GetInformationResponse();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        context.Response.StatusCode = 102;
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                    else if (resourcePath == "Success")
                    {
                        string responseString = GetSuccessResponse();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        context.Response.StatusCode = 201;
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                    else if (resourcePath == "Redirection")
                    {
                        string responseString = GetRedirectionResponse();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        context.Response.StatusCode = 308;
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                    else if (resourcePath == "ClientError")
                    {
                        string responseString = GetClientErrorResponse();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        context.Response.StatusCode = 403;
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                    else if (resourcePath == "ServerError")
                    {
                        string responseString = GetServerErrorResponse();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        context.Response.StatusCode = 503;
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        context.Response.OutputStream.Close();
                    }

                    if (listener.IsListening == false)
                    {
                        break;
                    }
                }
            });

            requestHandlerThread.Start();

            Console.WriteLine("Press any key to stop listening...");
            Console.ReadKey();

            listener.Stop();

            static string GetMyName()
            {
                return "Maksim";
            }

            static string GetInformationResponse()
            {
                return "This is an Informational response.";
            }

            static string GetSuccessResponse()
            {
                return "This is a Success response.";
            }

            static string GetRedirectionResponse()
            {
                return "This is a Redirection response.";
            }

            static string GetClientErrorResponse()
            {
                return "This is a client error response.";
            }

            static string GetServerErrorResponse()
            {
                return "This is a server error response.";
            }

        }
    }
}
