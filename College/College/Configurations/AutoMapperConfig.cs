using AutoMapper;
using College.Data;
using College.Model;

namespace College.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //To Map from one class to another
            //CreateMap<Student, StudentDTO>();
            //CreateMap<StudentDTO, Student>();

            //To Map the classes as above in sinle line
            CreateMap<StudentDTO, Student>().ReverseMap();
            //Config for differently named property
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n =>n.Name, opt => opt.MapFrom(x =>x.StudentName)).;

            //Config for Ignoring property
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.StudentName, opt => opt.Ignore());

            //Config for making meaningful otput on null values
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "No Address Found" : n.Address));
        }
    }
}
