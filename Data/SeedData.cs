using GLOBAL_MART.Models;

namespace GLOBAL_MART.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        // =========================
        // 1. TẠO ADMIN NẾU CHƯA CÓ
        // =========================
        if (!db.Users.Any(u => u.Email == "admin@globalmart.vn"))
        {
            db.Users.Add(new User
            {
                FullName = "Quản trị viên",
                Email = "admin@globalmart.vn",
                Password = "Admin@123",
                Role = "Admin"
            });
        }

        // =========================
        // 2. TẠO DANH MỤC NẾU THIẾU
        // =========================
        string[] categoryNames =
        {
            "Điện tử",
            "Điện lạnh",
            "Gia dụng",
            "Nhà bếp"
        };

        foreach (var name in categoryNames)
        {
            if (!db.Categories.Any(c => c.Name == name))
            {
                db.Categories.Add(new Category
                {
                    Name = name,
                    Description = "Danh mục " + name
                });
            }
        }

        db.SaveChanges();

        // Lấy ID danh mục chính xác theo tên
        var categories = db.Categories.ToList();

        int dienTuId = categories.First(c => c.Name == "Điện tử").Id;
        int dienLanhId = categories.First(c => c.Name == "Điện lạnh").Id;
        int giaDungId = categories.First(c => c.Name == "Gia dụng").Id;
        int nhaBepId = categories.First(c => c.Name == "Nhà bếp").Id;

        // =========================
        // 3. DANH SÁCH 20 SẢN PHẨM
        // =========================
        var newProducts = new List<Product>
        {
            new Product
            {
                Name = "Quạt đứng",
                Price = 1290000,
                Stock = 15,
                CategoryId = giaDungId,
                Description = "Quạt đứng làm mát, thiết kế hiện đại, phù hợp phòng khách và phòng ngủ.",
                ImageUrl = "/images/products/fan.jpg"
            },

            new Product
            {
                Name = "Máy giặt mini",
                Price = 2490000,
                Stock = 15,
                CategoryId = dienLanhId,
                Description = "Máy giặt mini nhỏ gọn, tiện lợi cho gia đình và căn hộ nhỏ.",
                ImageUrl = "/images/products/mini-washer.jpg"
            },

            new Product
            {
                Name = "Nồi chiên không dầu 5L",
                Price = 1590000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Nồi chiên không dầu dung tích 5L, giúp chế biến món ăn tiện lợi.",
                ImageUrl = "/images/products/air-fryer.jpg"
            },

            new Product
            {
                Name = "Nồi cơm điện",
                Price = 890000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Nồi cơm điện gia đình, dễ sử dụng, giữ ấm tiện lợi.",
                ImageUrl = "/images/products/rice-cooker.jpg"
            },

            new Product
            {
                Name = "Ấm siêu tốc",
                Price = 490000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Ấm siêu tốc đun nước nhanh, phù hợp sử dụng hằng ngày.",
                ImageUrl = "/images/products/kettle.jpg"
            },

            new Product
            {
                Name = "Máy xay sinh tố",
                Price = 790000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Máy xay sinh tố dùng để xay trái cây, sinh tố và thực phẩm.",
                ImageUrl = "/images/products/blender.jpg"
            },

            new Product
            {
                Name = "Máy nướng bánh mì",
                Price = 650000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Máy nướng bánh mì tiện lợi cho bữa sáng.",
                ImageUrl = "/images/products/toaster.jpg"
            },

            new Product
            {
                Name = "Máy pha cà phê",
                Price = 1890000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Máy pha cà phê tại nhà, nhỏ gọn và tiện lợi.",
                ImageUrl = "/images/products/coffee-machine.jpg"
            },

            new Product
            {
                Name = "Máy hút bụi",
                Price = 1990000,
                Stock = 15,
                CategoryId = giaDungId,
                Description = "Máy hút bụi giúp vệ sinh nhà cửa nhanh chóng.",
                ImageUrl = "/images/products/vacuum.jpg"
            },

            new Product
            {
                Name = "Máy sấy tóc",
                Price = 390000,
                Stock = 15,
                CategoryId = giaDungId,
                Description = "Máy sấy tóc tiện lợi, nhiều chế độ sấy.",
                ImageUrl = "/images/products/hair-dryer.jpg"
            },

            new Product
            {
                Name = "Lò vi sóng",
                Price = 1990000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Lò vi sóng dùng hâm nóng và chế biến thực phẩm.",
                ImageUrl = "/images/products/microwave.jpg"
            },

            new Product
            {
                Name = "Lò nướng mini",
                Price = 1490000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Lò nướng mini dùng nướng bánh và các món ăn.",
                ImageUrl = "/images/products/mini-oven.jpg"
            },

            new Product
            {
                Name = "Máy đánh trứng",
                Price = 450000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Máy đánh trứng hỗ trợ làm bánh và chế biến món ăn.",
                ImageUrl = "/images/products/hand-mixer.jpg"
            },

            new Product
            {
                Name = "Máy làm bánh sandwich",
                Price = 590000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Máy làm bánh sandwich nhanh chóng, tiện lợi cho bữa sáng.",
                ImageUrl = "/images/products/sandwich-maker.jpg"
            },

            new Product
            {
                Name = "Nồi áp suất điện",
                Price = 1290000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Nồi áp suất điện đa năng giúp tiết kiệm thời gian nấu ăn.",
                ImageUrl = "/images/products/pressure-cooker.jpg"
            },

            new Product
            {
                Name = "Máy tạo độ ẩm",
                Price = 590000,
                Stock = 15,
                CategoryId = giaDungId,
                Description = "Máy tạo độ ẩm giúp không gian sống dễ chịu.",
                ImageUrl = "/images/products/humidifier.jpg"
            },

            new Product
            {
                Name = "Đèn bàn LED",
                Price = 350000,
                Stock = 15,
                CategoryId = giaDungId,
                Description = "Đèn bàn LED phù hợp học tập và làm việc.",
                ImageUrl = "/images/products/led-lamp.jpg"
            },

            new Product
            {
                Name = "Nồi lẩu điện mini",
                Price = 790000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Nồi lẩu điện mini dành cho cá nhân và gia đình nhỏ.",
                ImageUrl = "/images/products/mini-hotpot.jpg"
            },

            new Product
            {
                Name = "Máy ép trái cây",
                Price = 990000,
                Stock = 15,
                CategoryId = nhaBepId,
                Description = "Máy ép trái cây tiện lợi để chuẩn bị nước ép tại nhà.",
                ImageUrl = "/images/products/juicer.jpg"
            },

            new Product
            {
                Name = "Bàn ủi hơi nước",
                Price = 690000,
                Stock = 15,
                CategoryId = giaDungId,
                Description = "Bàn ủi hơi nước giúp làm phẳng quần áo nhanh chóng.",
                ImageUrl = "/images/products/steamer.jpg"
            }
        };

        // =========================
        // 4. THÊM HOẶC CẬP NHẬT SẢN PHẨM
        // =========================
        foreach (var product in newProducts)
        {
            var existingProduct = db.Products
                .FirstOrDefault(p => p.Name == product.Name);

            if (existingProduct == null)
            {
                // Chưa có thì thêm mới
                db.Products.Add(product);
            }
            else
            {
                // Đã có thì cập nhật sản phẩm cũ
                existingProduct.Price = product.Price;
                existingProduct.Stock = 15;
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.CategoryId = product.CategoryId;
            }
        }

        db.SaveChanges();
    }
}