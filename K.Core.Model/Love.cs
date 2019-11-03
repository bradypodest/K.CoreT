using System;

namespace K.Core.Model
{
    /// <summary>
    /// 这是爱     测试类
    /// </summary>
    public class Love
    {
        public virtual string SayLoveU()
        {
            return "I ♥ U";
        }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}
