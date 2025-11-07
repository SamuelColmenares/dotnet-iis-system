using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syac.Patient.Domain.Aggregates;

public class PatientAggregate
{
    public string Id { get; init; }

    public string FirstName { get; init; }
    public string LastName { get; init; }

    public int MyProperty { get; set; }

}
