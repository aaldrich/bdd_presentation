using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;

namespace BDD_Tests
{
    public class MachineTests : Observes<Machine1>
    {
		
    }

    public class Machine1
    {
    }

    public class when_test_action : MachineTests
    {
        Establish context = () =>
                            {
					
                            };
			
        Because b = () =>
                    {
					
                    };

        It should_test_assertion = () =>
                      {
                          
                      };
    }
}
