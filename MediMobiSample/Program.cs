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
                Identifier = new List<Identifier>
                {
                    new Identifier ("https://www.ehealth.fgov.be/standards/fhir/NamingSystem/ssin", "00.01.01-003.03")                    
                },
                Name = new List<HumanName> {HumanName.ForFamily("Doe").WithGiven("Jane")},
                Gender = AdministrativeGender.Female,
                Language = "nl-be",
                Address = new List<Address>
                {
                    new Address()
                    {
                        Line = new []{"Astridstraat 192"},
                        City  = "Zottegem",
                        PostalCode = "9620",
                        Use = Address.AddressUse.Home
                    }
                },
                Telecom = new List<ContactPoint>
                {
                    new ContactPoint(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Mobile,
                        "+32478123456"),
                    new ContactPoint(ContactPoint.ContactPointSystem.Email, ContactPoint.ContactPointUse.Home,
                        "Jane.Doe@mediligo-demo.com")
                }
                
            };

            var campus = new Location()
            {
                Id = "campus",
                Name = "Hospital X - campus Ghent",
                Address = new Address
                {
                    Line = new List<string>() {"Steenweg 37"},
                    PostalCode = "9000",
                    City = "Ghent"
                }
            };
            
            var location = new Location
            {
                Id="location",
                Name = "Wachtzaal Orthopedie",
                Alias = new []{"Route 37"},
                PartOf = new ResourceReference("#campus"),
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
                    new CodeableConcept("https://demohospital.mediligo.com/serviceType", "shoulder-first-consult"),
                    new CodeableConcept("http://mediligo.com/fhir/MediMobi/CodeSystem/medimobi-appointment-class",
                        "consultation-mobile")
                },
                Specialty = new List<CodeableConcept> {new CodeableConcept("http://snomed.info/sct", "394801008")},
                Start = new DateTimeOffset(2020, 10, 24, 10, 0, 0, TimeSpan.FromHours(1)),
                End = new DateTimeOffset(2020, 10, 24, 11, 0, 0, TimeSpan.FromHours(1)),
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