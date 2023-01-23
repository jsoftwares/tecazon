using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    /**This class helps to intercept & log microservices Requests/Responses messages in Elasticsearch & Kibana: this can be
     * useful for debuging, tracing, inspection & helping customers with request problem etc
     * We use Messaage Handler (a class that received an HTTP request & returns an HTTP response) which is the best way bcos we 
     * would not need to make  changes to our API controllers in other to log incominng request & their corresponding responses**/
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingDelegatingHandler> logger;

        public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Sending request to {Url}", request.RequestUri);

                var response = await base.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation("Received a success response from {Url}", response.RequestMessage.RequestUri);
                }
                else
                {
                    logger.LogWarning("Received a non-success status code {StatusCode} from {Url}",
                        (int)response.StatusCode, response.RequestMessage.RequestUri);
                }

                return response;
            }
            catch (HttpRequestException ex)
                when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionRefused)
            {
                var hostWithPort = request.RequestUri.IsDefaultPort
                    ? request.RequestUri.DnsSafeHost
                    : $"{request.RequestUri.DnsSafeHost}:{request.RequestUri.Port}";

                logger.LogCritical(ex, "Unable to connect to {Host}. Please check the " +
                                        "configuration to ensure the correct URL for the service " +
                                        "has been configured.", hostWithPort);
            }

            return new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                RequestMessage = request
            };
        }
    }
}
