using System;
using System.Collections.Generic;
using System.Linq;
using Kalev.Framework.Cqrs.EventSourcing.CommandDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Domain;
using Kalev.Framework.Cqrs.EventSourcing.EventDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects;
using test.MockObjects;
using Xunit;

namespace Kalev_Framework_Cqrs_EventSourcing_Test
{
    public class CommandHandlerFactoryTest
    {
        [Fact]
        public void CommandHandlerWithResponseShouldBeResolved()
        {
            //Given
            var commandHandlerFactory = new CommandHandlerFactory();
            
            var mockCommandHandlerWithResponse = new MockCommandHandlerWithResponse(new List<string>());
            var updateMockCommandHandlerWithResponse = new UpdateMockCommandHandlerWithResponse();
            
            //When
            commandHandlerFactory.Register<CreateAMockCommandWithResponse, int>(mockCommandHandlerWithResponse);
            commandHandlerFactory.Register<UpdateMockCommandWithResponse, int>(updateMockCommandHandlerWithResponse);

            var first_CommandHandler = commandHandlerFactory.Resolved<CreateAMockCommandWithResponse, int>();
            var second_commandHandler = commandHandlerFactory.Resolved<UpdateMockCommandWithResponse, int>();

            //Then
            Assert.IsType<MockCommandHandlerWithResponse>(first_CommandHandler);
            Assert.IsType<UpdateMockCommandHandlerWithResponse>(second_commandHandler);
        }
        [Fact]
        public void CommandHandlerShouldBeResolved()
        {
            //Given
            var commandHandlerFactory = new CommandHandlerFactory();
            
            var mockCommandHandler = new MockCommandHandler(new List<string>());
            var updateMockCommandHandler = new UpdateMockCommandHandler();            

            //When
            commandHandlerFactory.Register<CreateAMockCommand>(mockCommandHandler);
            commandHandlerFactory.Register<UpdateMockCommand>(updateMockCommandHandler);

            var first_CommandHandler = commandHandlerFactory.Resolved<CreateAMockCommand>();
            var second_commandHandler = commandHandlerFactory.Resolved<UpdateMockCommand>();
            //Then
            Assert.IsType<MockCommandHandler>(first_CommandHandler);
            Assert.IsType<UpdateMockCommandHandler>(second_commandHandler);
        }
    }
}
