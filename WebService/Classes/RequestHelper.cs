using System.Net.Http;

namespace WebService.Classes {
    public class RequestHelper {
        public HttpClient Client;

        public RequestHelper() {
            Client = new HttpClient();
        }
    }
}