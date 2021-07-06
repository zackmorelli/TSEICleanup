using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

/*
    TSEI Cleanup - ESAPI 16.0 version (7/6/2021)
    This is a simple ESAPI script with no interface. It automates the planning of TSEI (total skin electron) plans as much as ESAPI will allow.
    It is designed to be used after the TSEI template has been brought in to the plan, which includes the plan names and beams. It then will populate the names and comment fields of the beams.
    The application of this program is very specific. Because it is write-enabled, their need to be protections. It will only work if the plans "A TSEI Day 1" and "A TSEI Day 2" are present, meaning the dosimitrist has just brough in the plan template.
    This program is expressely written as a plug-in script for use with Varian's Eclipse Treatment Planning System, and requires Varian's API files to run properly.
    This program requires .NET Framework 4.6.1

    Copyright (C) 2021 Zackary Thomas Ricci Morelli
    
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.


    I can be contacted at: zackmorelli@gmail.com

*/



[assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    class Script
    {
        public Script() { }
        [MethodImpl(MethodImplOptions.NoInlining)]

        public void Execute(ScriptContext context)
        {
            Course course = context.Course;
            Patient patient = context.Patient;

            bool TSEI1check = false;
            bool TSEI2check = false;
            PlanSetup TSEI1 = course.PlanSetups.First();
            PlanSetup TSEI2 = course.PlanSetups.First();

            foreach(PlanSetup pl in course.PlanSetups)
            {
                if(pl.Id == "A TSEI day1")
                {
                    TSEI1 = pl;
                    TSEI1check = true;
                }
                else if(pl.Id == "A TSEI day2")
                {
                    TSEI2 = pl;
                    TSEI2check = true;
                }
            }

            if(TSEI1check == false || TSEI2check == false)
            {
                MessageBox.Show("This is not a valid TSEI plan!");
                return;
            }

            patient.BeginModifications();
            foreach(Beam Be in TSEI1.Beams)
            {
                switch (Be.Id)
                {
                    case "A.A":
                        Be.Name = "AP UL P70";
                        Be.Comment = "Platform: 70, Gantry: 113.0";
                        break;

                    case "A.B":
                        Be.Name = "AP LL P70";
                        Be.Comment = "Platform: 70, Gantry: 64.5";
                        break;

                    case "A.C":
                        Be.Name = "AP LR P150";
                        Be.Comment = "Platform: 150, Gantry: 64.5";
                        break;

                    case "A.D":
                        Be.Name = "AP UR P150";
                        Be.Comment = "Platform: 150, Gantry: 113.0";
                        break;

                    case "A.E":
                        Be.Name = "PA UL P150";
                        Be.Comment = "Platform: 150, Gantry: 113.0";
                        break;

                    case "A.F":
                        Be.Name = "PA LL P150";
                        Be.Comment = "Platform: 150, Gantry: 64.5";
                        break;

                    case "A.G":
                        Be.Name = "PA LR P70";
                        Be.Comment = "Platform: 70, Gantry: 64.5";
                        break;

                    case "A.H":
                        Be.Name = "PA UR P70";
                        Be.Comment = "Platform: 70, Gantry: 113.0";
                        break;
                }
            }

            foreach (Beam Be in TSEI2.Beams)
            {
                switch (Be.Id)
                {
                    case "A.I":
                        Be.Name = "AP UR P110";
                        Be.Comment = "Platform: 110, Gantry: 113.0";
                        break;

                    case "A.J":
                        Be.Name = "AP LR P110";
                        Be.Comment = "Platform: 110, Gantry: 64.5";
                        break;

                    case "A.K":
                        Be.Name = "AP LL P30";
                        Be.Comment = "Platform: 30, Gantry: 64.5";
                        break;

                    case "A.L":
                        Be.Name = "AP UL P30";
                        Be.Comment = "Platform: 30, Gantry: 113.0";
                        break;

                    case "A.M":
                        Be.Name = "PA UR P30";
                        Be.Comment = "Platform: 30, Gantry: 113.0";
                        break;

                    case "A.N":
                        Be.Name = "PA LR P30";
                        Be.Comment = "Platform: 30, Gantry: 64.5";
                        break;

                    case "A.O":
                        Be.Name = "PA LL P110";
                        Be.Comment = "Platform: 110, Gantry: 64.5";
                        break;

                    case "A.P":
                        Be.Name = "PA UL P110";
                        Be.Comment = "Platform: 110, Gantry: 113.0";
                        break;
                }
            }


            MessageBox.Show("Names and Comments have been added to all TSEI beams.");


        }










    }
}
