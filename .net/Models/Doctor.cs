using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace clinic_Manage.Models;

public partial class Doctor
{
    [Display(Name = "Doctor ID")]
    public int DoctorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialty { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

   

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department? Department { get; set; }

}
