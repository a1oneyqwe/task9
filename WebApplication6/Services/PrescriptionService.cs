using Microsoft.EntityFrameworkCore;
using WebApplication6.DBContext;
using WebApplication6.DTO;
using WebApplication6.Repositories;

namespace WebApplication6.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(PrescriptionDto prescriptionDto);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ApplicationDbContext _context;

    public PrescriptionService(IPatientRepository patientRepository, ApplicationDbContext context)
    {
        _patientRepository = patientRepository;
        _context = context;
    }

    public async Task AddPrescriptionAsync(PrescriptionDto prescriptionDto)
    {
        if (prescriptionDto.Medicaments.Count > 10)
        {
            throw new ArgumentException("A prescription can include a maximum of 10 medications.");
        }

        var patient = await _patientRepository.GetPatientByIdAsync(prescriptionDto.Patient.IdPatient) ??
                      new Patient { IdPatient = prescriptionDto.Patient.IdPatient, FirstName = prescriptionDto.Patient.FirstName };

        var medicaments = await _context.Medicaments
            .Where(m => prescriptionDto.Medicaments.Select(md => md.IdMedicament).Contains(m.IdMedicament))
            .ToListAsync();

        if (medicaments.Count != prescriptionDto.Medicaments.Count)
        {
            throw new ArgumentException("One or more medicaments do not exist.");
        }

        var prescription = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            Patient = patient,
            IdDoctor = prescriptionDto.Doctor.IdDoctor,
            PrescriptionMedicaments = prescriptionDto.Medicaments.Select(md => new PrescriptionMedicament
            {
                IdMedicament = md.IdMedicament,
                Dose = md.Dose,
                Description = md.Description
            }).ToList()
        };

        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }
}