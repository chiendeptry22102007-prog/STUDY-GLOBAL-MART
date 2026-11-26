using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GLOBAL_MART.Controllers
{
    [Route("AI")]
    public class AIController : Controller
    {
        [HttpPost("Chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            string? apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return Json(new
                {
                    success = false,
                    message = "Chưa cấu hình Gemini API Key."
                });
            }

            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return Json(new
                {
                    success = false,
                    message = "Bạn chưa nhập câu hỏi."
                });
            }

            try
            {
                using HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

                string websiteInstructions = @"
Bạn là GLOBAL AI, trợ lý thông minh chính thức của website STUDY MARK.

NHIỆM VỤ:
- Trả lời mọi câu hỏi liên quan đến website STUDY MARK.
- Trả lời bằng tiếng Việt, thân thiện, dễ hiểu, tự nhiên.
- Không trả lời máy móc, không lặp lại câu hỏi.
- Nếu khách hỏi cách sử dụng website, hãy hướng dẫn từng bước.

THÔNG TIN WEBSITE STUDY MARK:

1. Trang chủ:
- Là nơi giới thiệu cửa hàng và các sản phẩm.
- Khách có thể xem sản phẩm, ưu đãi và tìm sản phẩm cần mua.

2. Sản phẩm:
- STUDY MARK kinh doanh các mặt hàng điện tử, điện lạnh, gia dụng và đồ dùng nhà bếp.
- Có thể tư vấn lựa chọn sản phẩm dựa trên nhu cầu, ngân sách và số lượng người sử dụng.
- Không tự bịa tên sản phẩm, giá bán, số lượng tồn kho hoặc khuyến mãi cụ thể nếu chưa có dữ liệu.

3. Tài khoản:
- Khách có thể đăng ký tài khoản.
- Khách có thể đăng nhập để sử dụng các chức năng dành cho tài khoản.

4. Giỏ hàng:
- Khách có thể thêm sản phẩm vào giỏ hàng.
- Có thể xem giỏ hàng, kiểm tra sản phẩm và số lượng trước khi đặt mua.

5. Đặt hàng:
- Khách có thể tiến hành đặt hàng từ giỏ hàng.
- Hãy hướng dẫn khách kiểm tra sản phẩm và thông tin đặt hàng trước khi xác nhận.

6. Đơn hàng của tôi:
- Khách đăng nhập có thể vào mục Đơn hàng để xem đơn hàng của mình.
- Nếu khách không tìm thấy đơn hàng, hướng dẫn đăng nhập đúng tài khoản và vào mục Đơn hàng.

7. Đăng nhập và đăng ký:
- Hướng dẫn khách tìm các nút Đăng nhập hoặc Tạo tài khoản ở thanh menu.
- Nếu gặp lỗi tài khoản, hướng dẫn kiểm tra thông tin nhập và thử lại.

8. Quản trị:
- Website có khu vực Admin dành cho người được cấp quyền.
- Không hướng dẫn khách thông thường truy cập hoặc sử dụng chức năng quản trị.

CÁCH TRẢ LỜI:
- Câu hỏi đơn giản: trả lời ngắn gọn.
- Câu hỏi cần hướng dẫn: trình bày từng bước.
- Câu hỏi tư vấn mua hàng: hỏi ngân sách hoặc nhu cầu nếu cần.
- Câu hỏi về giá, tồn kho, đơn hàng cụ thể: nói rõ bạn chưa truy cập dữ liệu trực tiếp nếu chưa được cung cấp dữ liệu.
- Không bịa thông tin.
- Nếu câu hỏi không liên quan website hoặc mua sắm, vẫn có thể trả lời bình thường, lịch sự và hữu ích.
- Nếu không biết chắc, hãy nói rõ và đưa ra cách khách kiểm tra.

Bạn không phải nhân viên thật. Bạn là trợ lý AI của STUDY MARK.
";

                var body = new
                {
                    systemInstruction = new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = websiteInstructions
                            }
                        }
                    },
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new[]
                            {
                                new { text = request.Message }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        maxOutputTokens = 800
                    }
                };

                string json = JsonSerializer.Serialize(body);

                string url =
                    "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.8-flash:generateContent";

                using StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");

                using HttpResponseMessage response =
                    await client.PostAsync(url, content);

                string responseText =
                    await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.ServiceUnavailable
                    || response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Gemini hiện đang quá tải. Bạn hãy thử lại sau ít phút nhé."
                    });
                }

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Gemini API gặp lỗi HTTP "
                            + (int)response.StatusCode
                    });
                }

                using JsonDocument document =
                    JsonDocument.Parse(responseText);

                string answer = "";

                if (document.RootElement.TryGetProperty(
                    "candidates", out JsonElement candidates)
                    && candidates.GetArrayLength() > 0)
                {
                    JsonElement candidate = candidates[0];

                    if (candidate.TryGetProperty(
                        "content", out JsonElement responseContent)
                        && responseContent.TryGetProperty(
                            "parts", out JsonElement parts))
                    {
                        foreach (JsonElement part in parts.EnumerateArray())
                        {
                            if (part.TryGetProperty(
                                "text", out JsonElement text))
                            {
                                answer += text.GetString();
                            }
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(answer))
                {
                    answer = "GLOBAL AI chưa tạo được câu trả lời. Bạn hãy thử hỏi lại.";
                }

                return Json(new
                {
                    success = true,
                    message = answer
                });
            }
            catch (TaskCanceledException)
            {
                return Json(new
                {
                    success = false,
                    message = "GLOBAL AI phản hồi quá lâu. Bạn hãy thử lại."
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Có lỗi khi kết nối GLOBAL AI. Vui lòng thử lại."
                });
            }
        }
    }

    public class ChatRequest
    {
        public string? Message { get; set; }
    }
}