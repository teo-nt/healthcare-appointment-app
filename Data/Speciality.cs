﻿namespace HealthcareAppointmentApp.Data
{
    public class Speciality : BaseEntity
    {
        public string SpecialityName { get; set; } = string.Empty;

        public virtual ICollection<Doctor> Doctors { get; } = new HashSet<Doctor>();
    }
}
