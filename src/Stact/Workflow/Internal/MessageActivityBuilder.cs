﻿// Copyright 2010 Chris Patterson
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Stact.Workflow.Internal
{
	public class MessageActivityBuilder<TWorkflow, TInstance, TBody> :
		ActivityBuilder<TWorkflow, TInstance, TBody>
		where TWorkflow : class
		where TInstance : class
	{
		readonly StateBuilder<TWorkflow, TInstance> _builder;
		readonly MessageEvent<TBody> _event;
		MessageEventExceptionHandler<TInstance, TBody> _exceptionHandler;

		public MessageActivityBuilder(StateBuilder<TWorkflow, TInstance> builder, MessageEvent<TBody> eevent)
		{
			_builder = builder;
			_event = eevent;
		}

		public State<TInstance> State
		{
			get { return _builder.State; }
		}

		public Event<TBody> Event
		{
			get { return _event; }
		}

		public void AddExceptionHandler(EventExceptionHandler<TInstance, TBody> exceptionHandler)
		{
			if (_exceptionHandler == null)
				_exceptionHandler = new MessageEventExceptionHandler<TInstance, TBody>();

			_exceptionHandler.Add(exceptionHandler);
		}

		public void AddExceptionHandler(EventExceptionHandler<TInstance> exceptionHandler)
		{
			if (_exceptionHandler == null)
				_exceptionHandler = new MessageEventExceptionHandler<TInstance, TBody>();

			_exceptionHandler.Add(exceptionHandler);
		}

		public void AddActivity(Activity<TInstance> activity)
		{
			if (_exceptionHandler != null)
				activity = new MessageActivityExecutor<TInstance, TBody>(activity, _exceptionHandler);

			_builder.AddActivity(activity);
		}

		Event ActivityBuilder<TWorkflow, TInstance>.Event
		{
			get { return _event; }
		}

		public WorkflowModel<TWorkflow, TInstance> Model
		{
			get { return _builder.Model; }
		}
	}
}