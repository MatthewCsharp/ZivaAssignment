namespace WebService.Models {
    public class JsonResponse {
        public JsonResponse(bool success, string message) {
            Success = success;
            Message = message;
        }

        public JsonResponse(bool success) {
            Success = success;
            Message = "";
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}