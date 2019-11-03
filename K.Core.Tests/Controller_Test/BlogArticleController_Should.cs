using K.Core.Common;
using K.Core.Controllers;
using K.Core.IRepository;
using K.Core.IServices;
using K.Core.Model.Models;
using Moq;
using Xunit;
using System;

namespace K.Core.Tests
{
    public class BlogArticleController_Should
    {
        Mock<IBlogArticleServices> mockBlogSev = new Mock<IBlogArticleServices>();
        Mock<IRedisCacheManager> mockRedisMag = new Mock<IRedisCacheManager>();
        BlogController blogController;

        public BlogArticleController_Should()
        {
            mockBlogSev.Setup(r => r.Query());
            blogController = new BlogController(mockBlogSev.Object, mockRedisMag.Object);
        }

        [Fact]
        public void TestEntity()
        {
            BlogArticle blogArticle = new BlogArticle();

            Assert.True(blogArticle.bID >= 0);
        }
        [Fact]
        public async void AddEntity()
        {
            BlogArticle blogArticle = new BlogArticle()
            {
                bCreateTime = DateTime.Now,
                bUpdateTime = DateTime.Now,
                btitle = "xuint :test controller addEntity",

            };

            var res = await blogController.Post(blogArticle);

            Assert.True(res.success);

            var data = res.response;

            Assert.NotNull(data);
        }
    }
}
