using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Runtime.InteropServices;
using System;
using web_back.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace web_back.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            InitUser();
            InitDataCategoryCourse();
            InitBanner();
            InitVideo();
            InitCourse();
            /*InitQuizz();
            InitUserCourse();
            InitLesson();
            InitGroupLesson();*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public void InitBanner()
        {
            if (Banners.Any()) return;
            Banners.AddRange(new Banner().GetExampleData());
            SaveChanges();
        }

        private void InitDataCategoryCourse()
        {
            if (CategoriesCourse.Any()) return;
            var categoriesCourse = new CategoryCourse[]
            {
                new CategoryCourse( "Kỹ thuật đặt câu hỏi đào sâu Insight - Brandcam",false,false),
                new CategoryCourse( "Thiết kế UI UX bằng photoshop",false, false),
                new CategoryCourse( "Luyện thi HSK 1-2",false, false),
                new CategoryCourse( "Am Hiểu Rượu Vang Để Giao Tiếp Thành Công",false, false),
                new CategoryCourse( "BC201 - CHUYÊN ĐỀ VBA ",false, false),
                new CategoryCourse( "Facebook markerting từ A tới Z",false, false)

            };
            CategoriesCourse.AddRange(categoriesCourse);
            SaveChanges();
        }
        private void InitUser()
        {
            if (Users.Any()) return;
            var user = new User[]
            {
                new User("52100956@student.tdtu.edu.vn","l/8BMg/myQFpQvSsuebRpZgKbvKty8CqlDUcKA5x5FXiKi6CIs9hTxANVtT3UIwP",Role.User,"Elephant","47","https://firebasestorage.googleapis.com/v0/b/stydyonline-1ace6.appspot.com/o/images%2F6b4f3d476433bf6de622.jpg?alt=media&token=cfa073f9-e6f0-41ee-a6ef-56a9ea39736e"),
                new User("Administrator","IEzyq+CcuLby9xGImGTTqvAfb1FNd3FRezsr5H+Qyk4RHVURGL+OJ9/CBP9LQXG+",Role.Admin,"","","https://firebasestorage.googleapis.com/v0/b/stydyonline-1ace6.appspot.com/o/images%2FAdmin.png?alt=media&token=b64546a2-0941-46e0-bb8a-af7be3c2832e")
            };
            Users.AddRange(user);
            SaveChanges();
        }
        private  void InitUserCourse()
        {
            if(UserCourses.Any() || !Users.Any() || !Courses.Any()) return;
            var course =  Courses.ToListAsync();
            var user =  Users.ToListAsync();
            /*var groupLessons = GroupLessons.ToListAsync();*/
            var userCourse = new UserCourse[]
            {
                /*new UserCourse(user.Result[0],course.Result[1],groupLessons.Result[0]),
                new UserCourse(user.Result[0],course.Result[2],groupLessons.Result[1]),
                new UserCourse(user.Result[0],course.Result[3], groupLessons.Result[1]),
                new UserCourse(user.Result[0],course.Result[4], groupLessons.Result[1]),
                new UserCourse(user.Result[0],course.Result[5], groupLessons.Result[0])*/
                

            };
            UserCourses.AddRange(userCourse);
            SaveChanges();
        }
        private void InitCourse()
        {
            // Check if there are any existing courses
            if (Courses.Any() || !CategoriesCourse.Any()) return;

            var typeCourses = CategoriesCourse.ToListAsync();
            /*var groupLessons = GroupLessons.ToListAsync();*/

            var courses = new Course[]
            {
                new Course("Kỹ thuật đặt câu hỏi đào sâu Insight - Brandcam"
                ,"những kỹ năng / kỹ thuật trong phỏng vấn và gặp gỡ NTD để đi vào được thâm sâu trong tâm lý của họ và tìm kiếm insight.s"
                ,109.99
                ,"Nội dung chi tiết của khóa học này bao gồm 4 phần chính:\nNhững vấn đề bạn hay gặp phải khi phỏng vấn NTD.\nCác bước tiếp cận NTD.\nNhững công cụ/ kỹ thuật/ thủ thuật sử dụng trong phỏng vấn NTD"
                ,"Phân tích -  Những bước cơ bản biến thông tin phỏng vấn thành INSIGHT"
                ,"Các bạn sinh viên, đặc biệt các bạn học trong ngành marketing, quản trị kinh doanh, ngoại thương, truyền thông. Đặc biệt lúc bạn làm luận văn tốt nghiệp hay tham dự các cuộc thi marketing.\nHay đơn giản là những ai luôn muốn giải mã những điều mới lạ xung quanh mình."
                ,"https://res.cloudinary.com/dcyfz1emw/image/upload/v1727757276/l4iojr71qgagkob1khel.png"
                , "video1.mp4"
                , typeCourses.Result[0]
                ),
                new Course("Thiết kế UI UX bằng photoshop"
                ,"Thiết kế UI UX bằng photoshop theo chuẩn Material Design"
                ,209.99
                ,"Nắm Vững Nguyên Lý Thiết Kế: Hiểu và áp dụng các nguyên lý cơ bản về typography và màu sắc trong thiết kế theo chuẩn Material Design./nThực Hành Thiết Kế Layout và Elevation:Thực hành thiết kế layout và áp dụng nguyên lý elevation trong thiết kế ứng dụng.\nThiết Kế Các Đối Tượng UI Cơ Bản: Thiết kế và phân loại các button, toggle button, floating action button, và các đối tượng UI khác theo chuẩn Material Design.\nThiết Kế Khối Nội Dung và Các Thành Phần UI: Thiết kế khối nội dung (card), banner, chips, dialog, appbar, danh sách dữ liệu, menu dropdown, thanh navigation drawer, và menu ngang.\nThực Hành và Ứng Dụng: Thực hành thiết kế các bố cục ảnh và tổng kết lại các kiến thức đã học, áp dụng chúng vào các dự án thiết kế thực tế."
                ,"✅ Hướng dẫn chi tiết, cho người không có kinh nghiệm thiết kế ứng dụng và web thì vẫn có thể cho ra nhưng thiết kế đẹp và sử dụng thực tế được .\n✅ Phân tích kĩ và thực hành chi tiết từng bước theo kiểu cầm tay chỉ việc.\n✅ Đi sâu và phân nhỏ các đối tượng UI để có thiết kế chi tiết và chuẩn hóa theo Material Design"
                ,"Không cần kinh nghiệm chuyên môn, bạn sẽ được cầm tay chỉ việc và giải đáp những thắc mắc"
                ,"https://res.cloudinary.com/dcyfz1emw/image/upload/v1727757276/l4iojr71qgagkob1khel.png"
                , "video1.mp4"
                , typeCourses.Result[1]
                ),


                new Course("Luyện thi HSK 1-2"
                ,"Luyện thi HSK 1-2 một cách dễ dàng và thuận tiện"
                ,209.99
                ,"Bạn đã bao giờ tự hỏi bản thân rằng HSK là chứng chỉ gì chưa? Hay chỉ là biết rằng nó là một chứng chỉ về trình độ tiếng Trung.\nHiểu một cách đơn giản, chứng chỉ HSK là một trong những tài liệu quan trong khi đi xin việc tại các công ty Trung Quốc hay du học. Chứng chỉ HSK có giá trị trên toàn thế giới, nó giống như tiếng Anh, nó có hiệu lực trong 2 năm kể từ ngày cấp. Hiện nay, HSK có 6 cấp độ tương ứng với số điểm cao dần: HSK 1 – HSK 2: sơ cấp thấp, chưa được cấp chứng chỉ. HSK 3 – HSK 4: sơ cấp trung HSK 5 – HSK 6: cao cấp.\nVà:\nĐể thi đỗ chứng HSK thì không phải đơn giản. Chính vì thế, khóa học Thực chiến HSK 1-2 của giảng viên Nguyễn Danh Vân trên UNICA sẽ giúp bạn đạt được điều đó với lộ trình ôn luyện hiệu quả qua các bài giảng ngữ pháp, từ vựng bổ trợ cho cả 4 kỹ năng nghe, nói, đọc, viết. \nKhóa học được hướng dẫn trực tiếp bởi Giảng viên Nguyễn Danh Vân giàu kinh nghiệm chuyên sâu về giải đề HSK cùng các kỹ năng, tips làm bài để đạt kết quả cao trong kỳ thi năng lực tiếng Trung."
                ,"Sau khóa học, bạn sẽ có thể hiểu và phân tích cấu trúc bài thi HSK và biết được các lỗi, bẫy, kỹ xảo hay gặp trong đề thi để phản xạ nhanh. Từ đó, rút ra cho mình những bài học ngoại ngữ online, bí kíp ôn luyện phù hợp với bản thân, có nền tảng và sự tự tin để chinh phục bài thi HSK một cách nhanh chóng và hiệu quả. "
                ,"Cần biết tiếng trung ở mức cơ bản"
                ,"https://res.cloudinary.com/dcyfz1emw/image/upload/v1724320751/x4fgqjedyvc4qjylil6v.png"
                , "video1.mp4"
                , typeCourses.Result[2]
                ),

                new Course("Am Hiểu Rượu Vang Để Giao Tiếp Thành Công"
                ,"Cung cấp những kiến thức có tính ứng dụng cao về rượu vang"
                ,209.99
                ,"Khóa học Am Hiểu Rượu Vang Để Giao Tiếp Thành Công bao gồm 25 video cung cấp những kiến thức có tính ứng dụng cao về rượu vang như: cách chọn một chai vang ngon dựa trên việc đọc hiểu nhãn chai, cách thức thử nếm và mô tả cảm nhận về hương vị khi uống 1 chai vang, cách chém 1 chai vang trên bàn tiệc rượu vang cũng như những kiến thức khác về văn hóa bàn tiệc chưa từng được chia sẻ. Tham gia khóa học, học viên được:Được tư vấn 1:1 với giảng viên ngay trong khóa học.\nĐược tham gia vào một cộng đồng những người yêu vang do chuyên gia Tô Việt làm chủ tịch\nĐược tham gia các buổi giao lưu, chia sẻ về rượu vang online/ offline hàng tháng cùng chuyên gia Tô Việt và nhiều chuyên gia khách mời khác.  "
                ,"Được có những kiến thức chuyên sâu về rượu vang, am hiểu hơn về việc chọn lựa rượu"
                ,"Không cần kinh nghiệm chuyên môn, Được tư vấn 1:1 với giảng viên ngay trong khóa học."
                ,"https://res.cloudinary.com/dt3z9ailp/image/upload/v1722500836/l9n4zrarapggh4eaqe4d.png"
                , "video1.mp4"
                , typeCourses.Result[3]
                ),

                new Course("BC201 - CHUYÊN ĐỀ VBA : SỬ DỤNG VBA LÀM BÁO CÁO"
                ,"Với kinh nhiệm nhiều năm làm việc với VBA. Mình muốn chia sẽ với các bạn đam mê VBA kinh nhiệm mà mình đã tích lũy được khi đã từng viết rất nhiều ứng dụng nhỏ thuần VBA cho các doanh nghiệp."
                ,109.99
                ,"Khóa học này sẽ mang lại cho các bạn:\nCái nhìn tổng quan trước khi code một ứng dụng nào đó.\nKhi bắt đầu code sẽ biết nên làm thế nào ?\nKhi code nên dùng biến ra sao ?\nĐối với từng dạng báo cáo sẽ biết được hướng để giải quyết vấn đề.\nCode sao không giật lag, không lỗi lung tung. Nếu có lỗi biết tìm đến đâu để điều chỉnh\nKhi cần thay đổi cấu trúc dữ liệu, bổ sung báo cáo đều có thể sửa lại code nhanh chóng và dễ dàng.\nCác học viên có vấn đề gì chưa hiểu thì PM trực tiếp mình sẽ giải đáp rõ ràng và nhiệt tình. Trong khóa học có rất nhiều ví dụ thực tế cộng với các bài tập mang hướng gợi mở rất lớn hy vọng phần nào đó giúp các bạn nâng cao công lực VBA để trợ giúp cho công việc hàng ngà"
                ," Cái nhìn tổng quan trước khi code một ứng dụng nào đó."
                ,"Cần có kiến thức về VBA cơ bản.\nMáy tính có kết nối mạng internet và trình duyệt Web\nMáy tính có cài đặt phần mềm Excel từ phiên bản 2003 trở lên, tốt nhất là Excel 2007 trở lên\nKiến thức mục tiêu\nTổ chức dữ liệu để xây dựng hàng loạt báo cáo\nỨng dụng Excel viết ra đảm bảo các yếu tố : Ổn định, nhanh, không giật lag, dễ dàng thay đổi cấu trúc dữ liệu, bảo trì , sữa lỗi...\nCode VBA khoa học và ngay ngắn và đúng hướng"
                ,"https://res.cloudinary.com/dcyfz1emw/image/upload/v1730777821/oxntarqjjbejh2stmuqc.jpg"
                , "video1.mp4"
                , typeCourses.Result[4]
                ),

                new Course("Facebook markerting từ A tới Z"
                ,"Tham gia khóa học Facebook Ads cùng chuyên gia Hồ Ngọc Cương “Facebook Marketing từ A - Z” ngay hôm nay để nhanh chóng tiếp cận tới khách hàng và đưa doanh nghiệp của bạn lên đỉnh cao mới trên thương trường Facebook. "
                ,309.99
                ,"khóa học marketing online cơ bản đến nâng cao để đáp ứng nhu cầu học và phát triển marketing cho doanh nghiệp và cá nhân."
                ,"Khóa học quảng cáo Facebook Marketing từ A - Z có gì dành cho bạn?\nChi tiết 7 dạng quảng cáo cơ bản trên Facebook và ứng dụng thông minh cho từng chiến dịch hiệu quả.\nChiến thuật xây dựng và phát triển Fanpage đỉnh cao phục vụ cho chạy Facebook Ads thành công.\nCách lên một chiến dịch Facebook Ads chuẩn.\nCách để đánh giá và tối ưu quảng cáo tốt nhất.\nCách để khai thác công cụ Remarketing Facebook tăng doanh số từ khách hàng cũ.\nHướng dẫn cách sử dụng các công cụ phân tích đo lường hiệu quả của Facebook Ads."
                ,"Với sự hướng dẫn chi tiết và dễ hiểu của giảng viên - chuyên gia Hồ Ngọc Cương, học viên hoàn toàn có thể tự học Facebook Marketing, sau khi hoàn thành khóa học này học viên hoàn toàn có thể tự tin đem về doanh thu “khủng” trên sân chơi Facebook vạn người với những chiến dịch quảng cáo hiệu quả nhất.."
                ,"https://res.cloudinary.com/dcyfz1emw/image/upload/v1724753574/beqfmqsm298dp6klsgnt.png"
                , "video1.mp4"
                , typeCourses.Result[5]
                ),

                new Course(
                    "Kỹ Thuật Quay Phim Cơ Bản",
                    "Khóa học cung cấp kiến thức nền tảng về quay phim, bao gồm cách sử dụng máy quay, ánh sáng, góc quay và âm thanh để tạo ra video chất lượng cao. Học viên sẽ học cách tối ưu hóa thiết bị sẵn có và làm quen với các kỹ thuật quay phim cơ bản để bắt đầu sự nghiệp quay phim hoặc ghi lại những khoảnh khắc trong cuộc sống.",
                    89.99,
                    "Bắt đầu sự nghiệp quay phim chuyên nghiệp với các kỹ thuật cơ bản",
                    "Những ai mới bắt đầu với quay phim, không yêu cầu kinh nghiệm trước đó",
                    "Không yêu cầu kiến thức quay phim trước, chỉ cần có máy quay hoặc điện thoại với camera",
                    "https://hmedia.com.vn/wp-content/uploads/2022/09/khoa-hoc-quay-va-dung-phim-chuyen-nghiep-tai-bien-hoa-dong-nai-6.jpg",
                    "basic_filming_intro.mp4",
                    typeCourses.Result[5]
                ),
                new Course(
                    "Chỉnh Sửa Ảnh Với Adobe Lightroom",
                    "Khóa học hướng dẫn cách chỉnh sửa ảnh chuyên nghiệp với Adobe Lightroom, bao gồm các công cụ xử lý màu sắc, điều chỉnh ánh sáng, cắt ghép ảnh và hiệu ứng đặc biệt. Học viên sẽ học cách tối ưu hóa từng chi tiết trong bức ảnh để tạo ra những sản phẩm chất lượng cao, ấn tượng và độc đáo.",
                    59.99,
                    "Tạo ra những bức ảnh đẹp mắt và ấn tượng nhờ kỹ năng chỉnh sửa chuyên nghiệp",
                    "Nhiếp ảnh gia mới hoặc những ai đam mê chụp ảnh muốn cải thiện kỹ năng chỉnh sửa",
                    "Kiến thức cơ bản về nhiếp ảnh và kỹ năng sử dụng máy tính",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQrlk_aJGUTsUN-CEkwhJva8P6TrSvmM1Viyg&s",
                    "lightroom_intro.mp4",
                    typeCourses.Result[1]
                ),
                new Course(
                    "Phát Triển Ứng Dụng Di Động Với React Native",
                    "Khóa học chi tiết về cách phát triển ứng dụng di động đa nền tảng bằng React Native, từ thiết kế giao diện đến triển khai tính năng và kết nối với cơ sở dữ liệu. Học viên sẽ học cách xây dựng các ứng dụng tương tác, hiệu quả và dễ dàng duy trì trên cả iOS và Android.",
                    139.99,
                    "Xây dựng ứng dụng di động đa nền tảng dễ dàng và hiệu quả",
                    "Nhà phát triển phần mềm, sinh viên ngành công nghệ thông tin",
                    "Kiến thức cơ bản về JavaScript và lập trình web",
                    "https://niithanoi.edu.vn/pic/Product/uudai.jpg",
                    "react_native_intro.mp4",
                    typeCourses.Result[2]
                ),
                new Course(
                    "Xây Dựng Thương Hiệu Cá Nhân Trên LinkedIn",
                    "Khóa học hướng dẫn cách xây dựng thương hiệu cá nhân chuyên nghiệp trên LinkedIn, từ cách viết profile thu hút, tạo nội dung chất lượng đến kết nối hiệu quả với các chuyên gia trong ngành. Học viên sẽ học cách tạo ấn tượng đầu tiên tốt và xây dựng mạng lưới chuyên nghiệp trên nền tảng LinkedIn.",
                    69.99,
                    "Nắm bắt cơ hội nghề nghiệp thông qua thương hiệu cá nhân mạnh mẽ trên LinkedIn",
                    "Người đi làm, sinh viên chuẩn bị đi làm, hoặc chuyên gia muốn mở rộng mạng lưới",
                    "Kiến thức cơ bản về mạng xã hội và LinkedIn",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSPcfZzenfXAq2jVzp66DUonp67oi-6RAVwmQ&s",
                    "linkedin_branding_intro.mp4",
                    typeCourses.Result[3]
                ),
                new Course(
                    "Kinh Doanh Dropshipping Từ A Đến Z",
                    "Khóa học toàn diện về kinh doanh dropshipping, từ việc lựa chọn sản phẩm, tìm nguồn cung ứng, đến quản lý đơn hàng và tối ưu hóa lợi nhuận. Học viên sẽ học cách vận hành một cửa hàng trực tuyến mà không cần tồn kho và tối ưu các công cụ quảng cáo để đạt hiệu quả cao nhất.",
                    149.99,
                    "Khám phá cơ hội kinh doanh trực tuyến mà không cần vốn lớn",
                    "Những ai muốn kinh doanh trực tuyến nhưng không có kinh nghiệm hoặc vốn đầu tư lớn",
                    "Không yêu cầu kinh nghiệm kinh doanh, chỉ cần sự kiên nhẫn và ý chí",
                    "https://khoahocsinhvien.com/wp-content/uploads/2024/06/Khoa-hoc-Dropshipping-K42-Moi-Nhat-Cung-Linh-Thach.jpg",
                    "dropshipping_intro.mp4",
                    typeCourses.Result[4]
                ),
                new Course(
                    "Hướng Dẫn Lập Trình Web Với HTML & CSS",
                    "Khóa học này giúp học viên làm quen với các công nghệ HTML và CSS, cung cấp kiến thức cơ bản và cách thực hành để tạo ra các trang web đơn giản, đẹp mắt và dễ sử dụng. Học viên sẽ học cách xây dựng cấu trúc, bố cục và định dạng cho các trang web theo nhu cầu cá nhân hoặc dự án nhỏ.",
                    39.99,
                    "Khởi đầu sự nghiệp lập trình web với các ngôn ngữ cơ bản",
                    "Những ai muốn bắt đầu sự nghiệp lập trình hoặc làm việc trong ngành IT",
                    "Không yêu cầu kinh nghiệm lập trình trước, chỉ cần máy tính kết nối internet",
                    "https://i.ytimg.com/vi/R6plN3FvzFY/maxresdefault.jpg",
                    "html_css_intro.mp4",
                    typeCourses.Result[1]
                ),
                new Course(
                    "Quản Trị Rủi Ro Trong Đầu Tư Tài Chính",
                    "Khóa học cung cấp kiến thức về quản trị rủi ro tài chính, từ cách phân tích rủi ro đến các phương pháp giảm thiểu thiệt hại. Học viên sẽ học cách đánh giá các yếu tố ảnh hưởng đến đầu tư và cách lập kế hoạch phòng ngừa để bảo vệ tài sản và tối đa hóa lợi nhuận.",
                    109.99,
                    "Trở thành nhà đầu tư thông minh với kỹ năng quản trị rủi ro vững chắc",
                    "Những người đầu tư tài chính hoặc có ý định tham gia thị trường tài chính",
                    "Hiểu biết cơ bản về tài chính và thị trường đầu tư",
                    "https://prbs.edu.vn/wp-content/uploads/2022/03/Bai-11-quan-tri-rui-ro.jpg",
                    "risk_management_intro.mp4",
                    typeCourses.Result[3]
                ),
                new Course(
                    "Học Vẽ Tranh Màu Nước Cơ Bản",
                    "Khóa học vẽ tranh màu nước dành cho người mới bắt đầu, bao gồm kỹ thuật cơ bản, cách pha màu, vẽ đối tượng và phong cảnh. Học viên sẽ được hướng dẫn chi tiết từng bước để hoàn thành tác phẩm từ những bài tập đơn giản đến nâng cao.",
                    44.99,
                    "Khám phá khả năng sáng tạo của bạn với nghệ thuật màu nước",
                    "Người yêu thích hội họa và muốn học vẽ màu nước",
                    "Không yêu cầu kinh nghiệm, chỉ cần các dụng cụ vẽ cơ bản",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQkz9RFpF2DD6CKjRkmf3eenTx5jrSZW3ym2A&s",
                    "watercolor_painting_intro.mp4",
                    typeCourses.Result[3]
                ),
                new Course(
                    "Giới Thiệu Về Khoa Học Dữ Liệu Với Python",
                    "Khóa học cung cấp kiến thức về khoa học dữ liệu với Python, từ cách xử lý dữ liệu đến các thuật toán học máy cơ bản. Học viên sẽ học cách phân tích, trực quan hóa dữ liệu và xây dựng các mô hình dự đoán sử dụng các công cụ phổ biến như pandas, numpy và scikit-learn.",
                    89.99,
                    "Tìm hiểu các kỹ năng phân tích dữ liệu và mô hình hóa với Python",
                    "Những ai muốn bắt đầu trong lĩnh vực khoa học dữ liệu",
                    "Kiến thức cơ bản về Python hoặc lập trình cơ bản",
                    "https://bkacad.edu.vn/upload_images/images/H%E1%BB%87%20ch%E1%BB%A9ng%20ch%E1%BB%89/PYTHONKIDS.jpg",
                    "data_science_python_intro.mp4",
                    typeCourses.Result[2]
                ),
                new Course(
                    "Cơ Bản Về Tư Duy Thiết Kế (Design Thinking)",
                    "Khóa học này giúp học viên hiểu và áp dụng tư duy thiết kế (Design Thinking) vào việc giải quyết vấn đề và phát triển sản phẩm sáng tạo. Nội dung bao gồm các bước từ thấu cảm người dùng, xác định vấn đề, hình thành ý tưởng, đến xây dựng và thử nghiệm sản phẩm mẫu.",
                    79.99,
                    "Tư duy sáng tạo và chiến lược giải quyết vấn đề hiệu quả",
                    "Những ai muốn cải thiện kỹ năng tư duy sáng tạo và giải quyết vấn đề",
                    "Không yêu cầu kinh nghiệm, chỉ cần sự sáng tạo và tinh thần học hỏi",
                    "https://adcacademy.vn/wp-content/uploads/2017/05/ADC_designthinking_poster.jpg",
                    "design_thinking_intro.mp4",
                    typeCourses.Result[1]
                ),

                new Course(
                    "Kỹ Năng Giao Tiếp Trong Môi Trường Công Sở",
                    "Khóa học giúp học viên phát triển kỹ năng giao tiếp hiệu quả trong môi trường công sở, từ việc lắng nghe chủ động, giao tiếp không lời, đến cách trình bày ý kiến một cách thuyết phục. Ngoài ra, khóa học cũng bao gồm các kỹ năng xử lý xung đột và xây dựng mối quan hệ tốt với đồng nghiệp và cấp trên.",
                    59.99,
                    "Nâng cao kỹ năng giao tiếp chuyên nghiệp và hiệu quả",
                    "Những ai làm việc trong môi trường công sở hoặc sắp vào môi trường công sở",
                    "Không yêu cầu kinh nghiệm, chỉ cần sẵn sàng học hỏi",
                    "https://ieit.vn/wp-content/uploads/2024/07/NGHE-THUAT-GIAO-TIEP-2-1024x576.jpg",
                    "office_communication_intro.mp4",
                    typeCourses.Result[3]
                ),
                new Course(
                    "Tiếng Tây Ban Nha Cho Người Mới Bắt Đầu",
                    "Khóa học dành cho những người mới học tiếng Tây Ban Nha, từ phát âm, ngữ pháp cơ bản đến từ vựng và giao tiếp hàng ngày. Học viên sẽ được hướng dẫn phát triển kỹ năng nghe, nói, đọc, viết để giao tiếp cơ bản và hiểu ngữ cảnh văn hóa của các nước nói tiếng Tây Ban Nha.",
                    69.99,
                    "Khám phá ngôn ngữ và văn hóa của các nước nói tiếng Tây Ban Nha",
                    "Người mới bắt đầu học tiếng Tây Ban Nha, không yêu cầu kinh nghiệm trước đó",
                    "Không cần kinh nghiệm ngoại ngữ, chỉ cần tinh thần học hỏi",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbWc3zxEnx0DCWtfVCq_eIrNeSOmJTjpnmoA&s",
                    "spanish_intro.mp4",
                    typeCourses.Result[4]
                ),
                new Course(
                    "Lập Kế Hoạch Dự Án Bằng Phương Pháp Agile",
                    "Khóa học hướng dẫn cách lập kế hoạch và quản lý dự án theo phương pháp Agile, bao gồm các công cụ và kỹ thuật quản lý như Scrum, Kanban và các nguyên tắc Agile. Học viên sẽ học cách tạo ra các lộ trình dự án linh hoạt, đảm bảo sự minh bạch và dễ thích ứng với thay đổi trong suốt quá trình thực hiện.",
                    119.99,
                    "Tối ưu hóa dự án của bạn với phương pháp Agile linh hoạt",
                    "Quản lý dự án, đội nhóm phát triển sản phẩm hoặc những ai muốn cải thiện kỹ năng quản lý",
                    "Kiến thức cơ bản về quản lý dự án là một lợi thế",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQPFPqRkQ2kfANQRZojKH0vsZrWjM3YoIPdVw&s",
                    "agile_project_management_intro.mp4",
                    typeCourses.Result[2]
                ),
                new Course(
                    "Thiết Kế Đồ Họa 3D Với Blender",
                    "Khóa học này cung cấp các kiến thức cơ bản đến nâng cao về thiết kế đồ họa 3D bằng Blender, bao gồm cách tạo dựng mô hình, ánh sáng, kết cấu và hoạt hình. Học viên sẽ học cách xây dựng các dự án 3D từ các đối tượng cơ bản đến các tác phẩm phức tạp hơn và áp dụng vào các ngành công nghiệp như game, phim, và quảng cáo.",
                    149.99,
                    "Biến ý tưởng thành hình ảnh sống động qua thiết kế 3D",
                    "Người mới bắt đầu muốn học thiết kế đồ họa 3D hoặc các nghệ sĩ kỹ thuật số",
                    "Không yêu cầu kinh nghiệm, chỉ cần máy tính đủ mạnh để chạy Blender",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSZijOXLiiakZMkcPgNYux3UA5NkVlLYftSQg&s",
                    "blender_3d_intro.mp4",
                    typeCourses.Result[3]
                ),
                new Course(
                    "Phân Tích Dữ Liệu Kinh Doanh Với Excel",
                    "Khóa học cung cấp kiến thức về cách sử dụng Excel để phân tích dữ liệu kinh doanh, bao gồm các công cụ và hàm nâng cao như Pivot Table, biểu đồ, và công thức phức tạp. Học viên sẽ học cách xử lý dữ liệu lớn, tìm hiểu xu hướng kinh doanh và lập các báo cáo chi tiết để hỗ trợ quyết định kinh doanh.",
                    89.99,
                    "Thành thạo Excel để phân tích và tối ưu hóa dữ liệu kinh doanh",
                    "Chuyên viên kinh doanh, quản lý và những ai muốn cải thiện kỹ năng phân tích",
                    "Kiến thức cơ bản về Excel là một lợi thế",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSSLpqPOesAppr8L9tijCa4K_FWG7t-AUNHoQ&s",
                    "excel_data_analysis_intro.mp4",
                    typeCourses.Result[2]
                ),


                new Course(
                    "Python cơ bản cho người mới bắt đầu",
                    "Học cách lập trình Python từ cơ bản, bao gồm cú pháp, cấu trúc dữ liệu và các khái niệm quan trọng khác.",
                    209.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                   " https://firebasestorage.googleapis.com/v0/b/appmapdemo-b2a39.appspot.com/o/python_course1.png?alt=media&token=b33921b2-3016-42f6-9c96-1acd9951b35a",
                          "//demo.mp5"
                    , typeCourses.Result[4]),
                new Course(
                    "Python nâng cao: Lập trình đối tượng và ứng dụng thực tế",
                    "Tiếp tục học về Python với các khái niệm nâng cao như lập trình đối tượng và áp dụng trong các dự án thực tế.",
                    390.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                    "https://firebasestorage.googleapis.com/v0/b/appmapdemo-b2a39.appspot.com/o/python_course2.png?alt=media&token=4c2b0e3d-b737-4454-81c5-1204aa31bf88",
                            "//demo.mp5"
                    , typeCourses.Result[4]),

                new Course(
                    "Python và Machine Learning",
                    "Học cách sử dụng Python cho machine learning và deep learning với thư viện như TensorFlow và Keras.",
                    490.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                    "https://firebasestorage.googleapis.com/v0/b/appmapdemo-b2a39.appspot.com/o/python_course3.png?alt=media&token=755a4640-6507-4f56-8a3d-d9bd1f3369cd",
                    "//demo.mp5"
                    , typeCourses.Result[4]),

                new Course(
                    "Web Development với Python và Django",
                    "Học cách xây dựng ứng dụng web sử dụng Python và framework Django từ đầu đến cuối.",
                    590.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                    "https://firebasestorage.googleapis.com/v0/b/appmapdemo-b2a39.appspot.com/o/python_course4.png?alt=media&token=f6f5bc68-107e-4701-9f33-4a0f48afdf6a",
                    "//demo.mp5"
                    , typeCourses.Result[4]),

                new Course(
                    "Python và Data Analysis",
                    "Học cách sử dụng Python cho phân tích dữ liệu và trực quan hóa với các thư viện như Pandas và Matplotlib.",
                    690.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                    "https://firebasestorage.googleapis.com/v0/b/stydyonline-1ace6.appspot.com/o/images%2Fpython_course4.png?alt=media&token=240c1e93-0bcd-42b4-8c61-7ba8583e07d0",
                    "//demo.mp5"
                    , typeCourses.Result[4]),


                new Course(
                    "Marketing cơ bản: Nền tảng và chiến lược",
                    "Học cách xây dựng nền tảng và chiến lược marketing cơ bản để phát triển doanh nghiệp.",
                    490.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                    "https://firebasestorage.googleapis.com/v0/b/appmapdemo-b2a39.appspot.com/o/marketing_course1.png?alt=media&token=48197284-a003-4fcf-86d3-d273f139e658",
                    "//demo.mp5"
                    , typeCourses.Result[5]),

                new Course(
                    "Content Marketing và SEO",
                    "Học cách sử dụng content marketing và SEO để tăng tương tác và hiệu suất của trang web.",
                    590.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                    "https://firebasestorage.googleapis.com/v0/b/appmapdemo-b2a39.appspot.com/o/marketing_course2.png?alt=media&token=9de9bb73-4c8b-4281-a7ea-b26022ecc3ad",
                    "//demo.mp5"
                    , typeCourses.Result[5]),

                new Course(
                    "Quảng cáo trực tuyến và Google Ads",
                    "Học cách tạo và quản lý quảng cáo trực tuyến sử dụng Google Ads để tăng lưu lượng truy cập và doanh số bán hàng.",
                    690.99,
                    "F8 duqc nhåc tdi d moi ndi, d dåu c6 cd håi viéc låm cho\nghé IT vå c6 nhüng con ngudi yéu thich lap trinh F8 sé d", "Cåc kién thüc cd bån, nen m6ng cüa ngånh IT, Cåc m6 hinh, kién trüc cd bån khi trién khai üng\ndung, Cåc khåi niém, thuat nga c6t löi khi trién khai (fng\ndung, Hiéu hdn ve cåch intemet vå måy vi tinh hoat döng", "Hoån thånh kh6a hoc Javascript cd bån tai F8 hoäc dä nåm chåc Javascript cd bån",
                    "https://firebasestorage.googleapis.com/v0/b/appmapdemo-b2a39.appspot.com/o/marketing_course3.png?alt=media&token=caed6bdb-27fb-479a-83ca-cfbcb9258b56",
                    "//demo.mp5"
                    , typeCourses.Result[5]),




            };
            // Add initial courses if the database is empty
            Courses.AddRange(courses);
            SaveChanges();
        }

        private async void InitLesson()
        {
            if (Lessons.Any()) return;
            var videos =  VideoLessons.ToListAsync();
            var quizz = QuizzLessons.ToListAsync();

            var lessons = new Lesson[]
            {
                new Lesson(TypeLesson.Video, "Lesson 1", "This is the first lesson", videos.Result[0], 1),
                new Lesson(TypeLesson.Video, "Lesson 2", "This is the second lesson", videos.Result[1], 2),
                new Lesson(TypeLesson.Video, "Lesson 3", "This is the third lesson", videos.Result[2], 1),
                new Lesson(TypeLesson.Video, "Lesson 4", "This is the fourth lesson", videos.Result[3], 2),
                new Lesson(TypeLesson.Video, "Lesson 5", "This is the fifth lesson", videos.Result[4], 1)
            };

            Lessons.AddRange(lessons);
            SaveChanges();
        }
        private void InitGroupLesson()
        {
            if (GroupLessons.Any()) return;
            var lessons = Lessons.ToListAsync();

            var groupLessons = new GroupLesson[]
            {
                new GroupLesson("Group 1", new List<Lesson>
                {
                    lessons.Result[0],
                    lessons.Result[1]
                }),
                new GroupLesson("Group 2", new List<Lesson>
                {
                    lessons.Result[2],
                    lessons.Result[3]
                }),
                new GroupLesson("Group 3", new List<Lesson>
                {
                    lessons.Result[4]
                })
            };

            GroupLessons.AddRange(groupLessons);
            SaveChanges();
        }
        private void InitVideo()
        {
            if (VideoLessons.Any()) return;

            var videoLessons = new VideoLesson[]
            {
                new VideoLesson("https://www.youtube.com/watch?v=ayUYK4T1r5E", "ayUYK4T1r5E"),
                new VideoLesson("https://youtu.be/OlulrDOixEg?si=wjFE9jRmbtdrEloV", "OlulrDOixEg"),
                new VideoLesson("https://www.youtube.com/watch?v=ZmKlM5GE-uY", "ZmKlM5GE-uY"),
                new VideoLesson("https://www.youtube.com/watch?v=Q0CbN8sfihY", "Q0CbN8sfihY"),
                new VideoLesson("https://youtu.be/xvFZjo5PgG0", "xvFZjo5PgG0")
            };

            VideoLessons.AddRange(videoLessons);
            SaveChanges();
        }
        private void InitQuizz()
        {
            if(QuizzLessons.Any()) return;
            var quizzLessons = new QuizzLesson[]
            {
                 new QuizzLesson(
                    question: "What is the output of `console.log(typeof null);` in JavaScript?",
                    answers: new List<string> { "'object'", "'null'", "'undefined'", "'string'" },
                    correctAnswerIndex: 0,
                    explain: "In JavaScript, `typeof null` returns `'object'` due to a historical bug in the language.",
                    index: 1
                ),
                new QuizzLesson(
                    question: "Which method is used to find the length of a string in JavaScript?",
                    answers: new List<string> { ".size", ".length", ".count", ".len" },
                    correctAnswerIndex: 1,
                    explain: "In JavaScript, `.length` is used to get the number of characters in a string.",
                    index: 2
                ),
                new QuizzLesson(
                    question: "What does `NaN` stand for in JavaScript?",
                    answers: new List<string> { "Not a Number", "Negative and Null", "Not a Null", "No Action Needed" },
                    correctAnswerIndex: 0,
                    explain: "`NaN` stands for 'Not a Number', indicating a value that is not a legal number.",
                    index: 3
                ),
                new QuizzLesson(
                    question: "What is the default value of `undefined` in JavaScript?",
                    answers: new List<string> { "0", "null", "NaN", "undefined" },
                    correctAnswerIndex: 3,
                    explain: "In JavaScript, `undefined` means a variable has been declared but not yet assigned a value.",
                    index: 4
                )
            };
            QuizzLessons.AddRange( quizzLessons );
        }
        private void InitPost()
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CategoryCourse> CategoriesCourse { get; set; }
        public DbSet<SubItemPost> SubItems { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<NoteLesson> NoteLessons { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<VideoLesson> VideoLessons { get; set; }
        public DbSet<QuizzLesson> QuizzLessons { get; set;}
        public DbSet<PostLesson> PostLessons { get;set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<GroupLesson> GroupLessons { get; set; }
        



    }
}
