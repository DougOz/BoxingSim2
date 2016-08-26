using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;

namespace Weebul.Scripting
{
    public class Class1
    {
        private ScriptEngine m_engine = Python.CreateEngine();
private ScriptScope m_scope = null;
        public void DoSomething()
        {
            ScriptEngine engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            Object obj = new object(); 
            scope.SetVariable("myVar", 4);
            scope.SetVariable("global1", obj);
            scope.SetVariable("obj1", obj);
            scope.SetVariable("round", 3);
            string script = @"def foo(round):
    if round <3 : return '4H/8/8'
    if round == 3 : myVar = 67
    if round ==3 : return '4B/8/8'
    if global1 is obj1 : return '2/2/22'
    return '6/6/8'";
            ScriptSource ss = engine.CreateScriptSourceFromString(script);
            var v = ss.Execute<string>(scope);
            
            Debug.Print("Yo");

            script = @"def foo(round):
    if myVar == 4 : return 'global1'
    return 'not global1'";

            var sSource = engine.CreateScriptSourceFromString(script);
            sSource.Execute<string>(scope);

            string script2 = "foo(myVar)";
            ScriptSource ss2 = engine.CreateScriptSourceFromString(script2);
            var v2 = ss2.Execute(scope);
            Debug.Print("Again");

        }
    }
}
