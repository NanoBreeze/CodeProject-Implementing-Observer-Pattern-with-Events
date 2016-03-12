using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject();
            Observer observer1 = new Observer(subject);
            Observer observer2 = new Observer(subject);

            //subscribe the Observer instances to "subject"
            observer1.Subscribe();
            observer2.Subscribe();

            subject.NotifyObservers();  //both Observer instances' DoSomething(object sender, EventArgs e) method are called

            observer1.UnSubscribe();
            subject.NotifyObservers();  //only observer2's  DoSomething(object sender, EventArgs e) method is called because observer2 has unsubscribed from "subject"

            Console.ReadKey();
        }
    }

    public class Subject 
    {
        public event EventHandler eventHandler; //We can also consider using an auto-implemented property instead of a public field

        public void NotifyObservers()
        {
            if (eventHandler != null)   //Ensures that if there are no handlers, the event won't be raised
            {
                eventHandler(this, EventArgs.Empty);    //We caan also replace EventArgs.Empty with our own message
            }
        }
    }

    public class Observer 
    {
        Subject subject;

        public Observer(Subject subject)
        {
            this.subject = subject;
        }

        public void Subscribe()
        {
            subject.eventHandler += DoSomething;    //Every time the event is raised (from eventHandler(this,EventArgs.Empty);), DoSomething(...) is called
        }

        public void UnSubscribe()
        {
            subject.eventHandler -= DoSomething;    //Now, when the event is raised, DoSomething(...) is no longer called
        }

        private void DoSomething(object sender, EventArgs e)
        {
            Console.WriteLine("This Observer instance has received a notification from its associated Subject.");
        }
    }
}
