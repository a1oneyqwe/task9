namespace WebApplication6.DTO;

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public List<PrescriptionResponseDto> Prescriptions { get; set; }
}

public class PrescriptionDto
{
    public PatientDto Patient { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; }
}

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}

public class PrescriptionResponseDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentResponseDto> Medicaments { get; set; }
    public DoctorResponseDto Doctor { get; set; }
}

public class DoctorResponseDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}

public class MedicamentResponseDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}

public class PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
}
