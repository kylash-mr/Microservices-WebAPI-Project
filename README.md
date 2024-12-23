# CapstoneProject

A Medical Appointment Management System that uses a microservices architecture and developed using .NET Core 8.

1. Appointment Booking: Patients can book, cancel, and reschedule appointments.

2. Doctor Management: Doctors can update their availability and check their schedules.

3. Patient Records: Patients and doctors can view medical history.

4. Billing Service: Handles billing for consultations and treatments.


# Key API Endpoints:

User Management Service:

· POST /api/users/register – Register new users.

· POST /api/users/login – Authenticate users.

· GET /api/users/{id} – Retrieve user details.

Appointment Service:

· POST /api/appointments – Book an appointment.

· PUT /api/appointments/{id} – Reschedule an appointment.

· DELETE /api/appointments/{id} – Cancel an appointment.

Doctor Service:

· GET /api/doctors – Get list of available doctors.

· PUT /api/doctors/{id}/availability – Update availability.

Patient Record Service:

· GET /api/records/{patientId} – Get patient medical history.

· POST /api/records – Add new medical record.

Billing Service:

· POST /api/billing – Process payment.

· GET /api/billing/invoice/{id} – Retrieve invoice.
