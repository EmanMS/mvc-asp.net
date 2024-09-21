using System;
using System.Collections.Generic;

namespace clinic_Manage.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? MedicalHistory { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

}
