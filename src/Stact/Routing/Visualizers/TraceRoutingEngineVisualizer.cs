// Copyright 2010 Chris Patterson
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
namespace Stact.Routing.Visualizers
{
    using System.Diagnostics;
    using Stact.Internal;


    public class TraceRoutingEngineVisualizer
    {
        public void Show(RoutingEngine engine)
        {
            Trace.WriteLine(new StringRoutingEngineVisitor(engine).ToString());
        }

        public void Show(ActorRef actor)
        {
            var inbox = actor as ActorInbox;
            if (inbox == null)
                return;

            Trace.WriteLine(new StringRoutingEngineVisitor(inbox.Engine).ToString());
        }
    }
}