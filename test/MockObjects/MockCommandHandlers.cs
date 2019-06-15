using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kalev.Framework.Cqrs.EventSourcing.CommandDrivers;
namespace test.MockObjects
{
    public class CreateAMockCommandWithResponse : ICommand<int>
    {
        public string Mock;
        public CreateAMockCommandWithResponse(string mock)
        {
            Mock = mock;
        }
        public Guid Guid => Guid.NewGuid();
    }

    public class MockCommandHandlerWithResponse : ICommandHandler<CreateAMockCommandWithResponse, int>
    {
        private List<string> _mockList;
        public MockCommandHandlerWithResponse(List<string> mockList)
        {
            _mockList = mockList;
        }
        public async Task<int> HandleAsync(CreateAMockCommandWithResponse command)
        {
            Task<int> addToMockList = Task.Run<int>(() => { _mockList.Add(command.Mock); return 1; }  );

            return await addToMockList;
        }
    }
    public class CreateAMockCommand : ICommand
    {
        public string Mock;
        public CreateAMockCommand(string mock)
        {
            Mock = mock;
        }
        public Guid Guid => Guid.NewGuid();
    }   

    public class MockCommandHandler : ICommandHandler<CreateAMockCommand>
    {
        private List<string> _mockList;        
        public MockCommandHandler(List<string> mockList)
        {
            _mockList = mockList;
        }
        public async Task HandleAsync(CreateAMockCommand command)
        {
            Task handle = Task.Run( ()=> { _mockList.Add(command.Mock); } );

            await handle;
        }
    }

    public class UpdateMockCommandWithResponse : ICommand<int>
    {
        public string _updatedMock;
        public UpdateMockCommandWithResponse(string updatedMock)
        {
            _updatedMock = updatedMock;
        }
        public Guid Guid => Guid.NewGuid();
    }

    public class UpdateMockCommandHandlerWithResponse : ICommandHandler<UpdateMockCommandWithResponse, int>
    {
        private string _mock;
        public async Task<int> HandleAsync(UpdateMockCommandWithResponse command)
        {
            Task<int> mockUpdated = Task.Run<int>(() => { _mock = command._updatedMock; return 1; }  );

            return await mockUpdated;            
        }
    }

    public class UpdateMockCommand : ICommand
    {
        public string _updatedMock;
        public UpdateMockCommand(string updatedMock)
        {
            _updatedMock = updatedMock;
        }

        public Guid Guid => Guid.NewGuid();
    }
    public class UpdateMockCommandHandler : ICommandHandler<UpdateMockCommand>
    {
        private string _mock;
        public async Task HandleAsync(UpdateMockCommand command)
        {
            Task mockUpdated = Task.Run(() => { _mock = command._updatedMock; }  );

            await mockUpdated;            
        }
    }    
}