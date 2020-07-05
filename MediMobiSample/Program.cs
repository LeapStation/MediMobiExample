using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace MediMobiSample
{
    class Program
    {
       static void Main(string[] args)
        {
            var resource = MediMobiExample();
            Console.Write(resource.ToJson());
        }

        private static Resource MediMobiExample()
        {
            var patient = new Patient
            {
                Id="patient",
                Name = new List<HumanName> {HumanName.ForFamily("Doe").WithGiven("Jane")},
                Gender = AdministrativeGender.Other,
                Language = "nl-be",
                Telecom = new List<ContactPoint>
                {
                    new ContactPoint(ContactPoint.ContactPointSystem.Email, ContactPoint.ContactPointUse.Home,
                        "Jane.Doe@mediligo-demo.com")
                }
                
            };

            var location = new Location
            {
                Id="location",
                Name = "Wachtzaal Orthopedie",
                Alias = new []{"Route 37"},
                Address = new Address
                {
                    Line = new List<string>() {"HealthStreet 37"},
                    PostalCode = "1234",
                    City = "Health Valley"
                }
            };


            var x = new Appointment
            {
                Contained = new List<Resource>
                {
                    patient,location 
                },
                Identifier =
                    new List<Identifier> {new Identifier("http://demohospital.mediligo.com/appointemnt", "1234")},
                Status = Appointment.AppointmentStatus.Booked,
                ServiceCategory = new List<CodeableConcept>
                {
                    new CodeableConcept("https://demohospital.mediligo.com/serviceType", "poly-orthopedics-consult"),
                    new CodeableConcept("http://mediligo.com/fhir/MediMobi/CodeSystem/medimobi-appointment-class",
                        "consultation-mobile")
                },
                Specialty = new List<CodeableConcept> {new CodeableConcept("http://snomed.info/sct", "394801008")},
                Start = new DateTimeOffset(2020, 10, 10, 10, 0, 0, TimeSpan.FromHours(1)),
                End = new DateTimeOffset(2020, 10, 10, 11, 0, 0, TimeSpan.FromHours(1)),
                Participant = new List<Appointment.ParticipantComponent>
                {
                    new Appointment.ParticipantComponent {Actor = new ResourceReference("#patient"),},
                    new Appointment.ParticipantComponent {Actor = new ResourceReference("#location"),}
                }
            };
            return x;
        }
    }
}