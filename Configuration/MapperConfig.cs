using AutoMapper;
using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;

namespace HealthcareAppointmentApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<User, DoctorReadOnlyDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Doctor!.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Doctor!.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Doctor!.Lastname))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Doctor!.City))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Doctor!.Address))
                .ForMember(dest => dest.StreetNumber, opt => opt.MapFrom(src => src.Doctor!.StreetNumber))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Doctor!.PhoneNumber))
                .ForMember(dest => dest.AppointmentDuration, opt => opt.MapFrom(src => src.Doctor!.AppointmentDuration))
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => src.Doctor!.Speciality));

            CreateMap<User, PatientReadOnlyDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Patient!.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Patient!.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Patient!.Lastname))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Patient!.Age))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient!.Gender))
                .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.Patient!.MedicalHistory))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Patient!.PhoneNumber));

            CreateMap<User, UserDetailsDTO>().ReverseMap();
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<User, UserReadOnlyDTO>();
            CreateMap<Speciality, SpecialityReadOnlyDTO>().ReverseMap();
            CreateMap<TimeSlot, AvailableTimeslotDTO>();
            CreateMap<Appointment, AppointmentReadOnlyDTO>();
        }
    }
}
