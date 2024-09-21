using System;
using System.Collections.Generic;

namespace clinic_Manage.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? ReasonForVisit { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public string? Status { get; set; }

    public decimal? Cost { get; set; }


    public virtual Doctor? Doctor { get; set; }


    public virtual Patient? Patient { get; set; }


}