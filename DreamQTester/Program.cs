using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DreamQEngine;
using DreamQEngine.Character;

using System.IO;

namespace DreamQTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = DreamQEngineTester.Properties.Resources.theboywholived.db;

            //Initiate novel from the db file.
            VisualNovel novel = new VisualNovel( );
            
            //Set currentScene for the first time to the default currentScene
            Scene currentScene = novel.currentScene;
            while(currentScene != null){
                //Set the currentDialogue for the first time to the startingDialogue
                Dialogue currentDialogue = currentScene.startingDialogue;
                Console.Out.WriteLine(currentScene.setup);
                Console.Out.WriteLine();
                while (currentDialogue != null)
                {
                    Console.Out.WriteLine(currentDialogue.actor.name+": "+ currentDialogue.text);
                    string choice = "0";

                    //If you have more than one choice, let them pick.
                    if(currentDialogue.choices.Length > 1) {
                        for (int i = 0; i < currentDialogue.choices.Length; i++)
                        {
                            Console.Out.WriteLine(i + ": " + currentDialogue.choices[i].displayText);
                        }
                        //Pick option
                        choice = Console.ReadLine();
                    }
                    //Otherwise, just show them the choice they get.
                    else {
                        Console.Out.WriteLine(currentDialogue.choices[0].displayText);
                        Console.ReadLine();
                    }

                    
                    int choiceInd = Convert.ToInt32(choice);
                    Option userOption = currentDialogue.choices[choiceInd];
                    Console.Out.WriteLine(userOption.text);
                    if (userOption.outcome.type == OutcomeType.dialogue)
                    {
                        currentDialogue = novel.getDialogue(userOption.outcome.reference);
                    }
                    else if (userOption.outcome.type == OutcomeType.scene)
                    {
                        currentDialogue = null;
                        currentScene = novel.getScene(userOption.outcome.reference);
                    }
                    else if (userOption.outcome.type == OutcomeType.gameEnd)
                    {
                        currentDialogue = null;
                        currentScene = null;
                    }
                }
            }
            Console.Out.WriteLine("The end.");
        }
    }
}
