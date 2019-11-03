using K.Core.Common;
using K.Core.Controllers;
using K.Core.IRepository;
using K.Core.IServices;
using K.Core.Model.Models;
using Moq;
using Xunit;
using System;
using System.Linq;
using K.Core.Services;
using AutoMapper;
using K.Core.Repository;

namespace K.Core.Tests
{
    public class BlogArticleService_Should
    {

        Mock<BlogArticleRepository> mockBlogRep = new Mock<BlogArticleRepository>();
        Mock<IMapper> mockMap = new Mock<IMapper>();
        BlogArticleServices _blogArticleServices;

        public BlogArticleService_Should()
        {
            //mockBlogRep.Setup(r => r.Query());
            _blogArticleServices = new BlogArticleServices(mockBlogRep.Object, mockMap.Object);
        }


        [Fact]
        public void BlogArticleServices_Test()
        {
            Assert.NotNull(_blogArticleServices);
        }


        [Fact]
        public async void Get_Blogs_Test()
        {
            var data = await _blogArticleServices.GetBlogs();

            Assert.True(data.Any());
        }

        [Fact]
        public async void Add_Blog_Test()
        {
            BlogArticle blogArticle = new BlogArticle()
            {
                bCreateTime = DateTime.Now,
                bUpdateTime = DateTime.Now,
                btitle = "xuint test title",
                bcontent = "xuint test content",
                bsubmitter = "xuint test submitter",
            };

            var BId = await _blogArticleServices.Add(blogArticle);

            Assert.True(BId > 0);
        }

        [Fact]
        public async void Delete_Blog_Test()
        {
            var deleteModel = (await _blogArticleServices.Query(d => d.btitle == "xuint test title")).FirstOrDefault();

            Assert.NotNull(deleteModel);

            var IsDel = await _blogArticleServices.Delete(deleteModel);

            Assert.True(IsDel);
        }
    }
}
