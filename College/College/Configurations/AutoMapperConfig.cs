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
        }
    }
}
