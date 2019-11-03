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
    public class Redis_Should
    {
        //IRedisCacheManager _redisCacheManager;

        //public Redis_Should(IRedisCacheManager redisCacheManager)
        //{
        //    _redisCacheManager = redisCacheManager;
        //}

        [Fact]
        public void Connect_Redis_Test()
        {
            RedisCacheManager _redisCacheManager = new RedisCacheManager();

            var redisBlogCache = _redisCacheManager.Get<object>("Redis.Blog");

            Assert.NotNull(redisBlogCache);
        }

    }
}
