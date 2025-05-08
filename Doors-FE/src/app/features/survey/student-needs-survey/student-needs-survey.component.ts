import { Component } from '@angular/core';
import {NgForOf} from "@angular/common";
import {TranslatePipe} from "@ngx-translate/core";

@Component({
  selector: 'app-student-needs-survey',
    imports: [
        NgForOf,
        TranslatePipe
    ],
  templateUrl: './student-needs-survey.component.html',
  styleUrl: './student-needs-survey.component.css'
})
export class StudentNeedsSurveyComponent {
  type: 'Student' | 'PrivateLandlord' | null = null;
  studentNeeds = [
    { title: 'STUDENT_NEEDS.NEED1_TITLE', description: 'STUDENT_NEEDS.NEED1_DESC' },
    { title: 'STUDENT_NEEDS.NEED2_TITLE', description: 'STUDENT_NEEDS.NEED2_DESC' },
    { title: 'STUDENT_NEEDS.NEED3_TITLE', description: 'STUDENT_NEEDS.NEED3_DESC' },
    { title: 'STUDENT_NEEDS.NEED4_TITLE', description: 'STUDENT_NEEDS.NEED4_DESC' },
    { title: 'STUDENT_NEEDS.NEED5_TITLE', description: 'STUDENT_NEEDS.NEED5_DESC' }
  ];

  onRespond(index: number, agree: boolean): void {
    const responseText = agree ? 'D’accord' : 'Pas d’accord';
    console.log(`Réponse à la carte ${index + 1} : ${responseText}`);
    // Ici tu peux gérer les réponses (ex: stockage temporaire, API, feedback visuel...)
  }
  onExtraSubmit() {

  }
}
