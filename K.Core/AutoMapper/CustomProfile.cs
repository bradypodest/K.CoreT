using AutoMapper;
using K.Core.Model;
using K.Core.Model.Models;

namespace K.Core.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap(typeof(Source<>), typeof(Destination<>));
            CreateMap(typeof(Destination<>), typeof(Source<>));

            CreateMap<SysUser, SysUserVM>();
            CreateMap<SysUserVM, SysUser>();


            //1.属性名称一致
            //原对象BlogArticle 转为 目的对象BlogViewModels
            //CreateMap<BlogArticle, BlogViewModels>();

            //2.属性名称不一致
            //CreateMap<Student, StudentViewModel>()
            // .ForMember(d => d.CountyName, o => o.MapFrom(s => s.County))
            // .ForMember(d => d.ProvinceName, o => o.MapFrom(s => s.Province))
            // ;

            //3.如果是还有子类的复杂类型
            #region  例子
            //  CreateMap<Student, StudentViewModel>()
            //.ForMember(d => d.County, o => o.MapFrom(s => s.Address.County))
            //.ForMember(d => d.Province, o => o.MapFrom(s => s.Address.Province))
            //.ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
            //.ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
            //;


            // public class Student : Entity
            //      {
            //          public string Name { get; private set; }
            //          public string Email { get; private set; }
            //          public string Phone { get; private set; }
            //          public DateTime BirthDate { get; private set; }
            //          public Address Address { get; private set; }
            //      }

            //      public class StudentViewModel
            //      {
            //          public Guid Id { get; set; }
            //          public string Name { get; set; }
            //          public string Email { get; set; }
            //          public DateTime BirthDate { get; set; }
            //          public string Phone { get; set; }
            //          public string Province { get; set; }
            //          public string City { get; set; }
            //          public string County { get; set; }
            //          public string Street { get; set; }
            //      }
            #endregion

        }
    }

    public class Source<T>
    {
        public T Value { get; set; }
    }

    public class Destination<T>
    {
        public T Value { get; set; }
    }
}
