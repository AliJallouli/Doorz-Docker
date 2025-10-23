import {Component, EventEmitter, Input, OnChanges, Output, SimpleChanges} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslatePipe} from '@ngx-translate/core';
import {SearchService} from '../../../core/services/search/search.service';
import {SearchMemoryService} from '../../../core/services/search/search-memory.service';
import {SearchIconComponent} from '../../../shared/icons/search-icon/search-icon.component';

@Component({
  selector: 'app-global-search',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    TranslatePipe,
    SearchIconComponent
  ],
  templateUrl: './global-search.component.html',
  styleUrls: ['./global-search.component.css']
})
export class GlobalSearchComponent implements OnChanges {
  searchForm: FormGroup;
  @Output() searchResults = new EventEmitter<any[]>();
  @Input() searchTitle: string = '';
  @Input() searchType: string='';
  autoSearchOnTypeChange: boolean = false;

  // 🔽 Ajoute ceci ici
  searchTypeToKey: Record<string, string> = {
    jobs: 'STUDENT_JOB',
    internships: 'INTERNSHIP',
    studies: 'STUDY',
    events: 'EVENT',
    aid: 'AID'
    ,kot:'KOT'
  };
  searchTypeToIconType: Record<string, 'job' | 'internship' | 'study' | 'event' | 'aid' | 'kot'> = {
    jobs: 'job',
    internships: 'internship',
    studies: 'study',
    events: 'event',
    aid: 'aid',
    kot:'kot'
  };



  constructor(private fb: FormBuilder,  private searchService: SearchService, private searchMemory: SearchMemoryService) {
    this.searchForm = this.fb.group({
      keywords: [''],
      location: ['']
    });

  }

  onSubmit(): void {
    const formValue = this.searchForm.value;
    const query = {
      keywords: (formValue.keywords ?? '').trim(),
      location: (formValue.location ?? '').trim()
    };

    this.searchService.search(this.searchType, query).subscribe(results => {

      this.searchMemory.setResults(this.searchType, results);
      this.searchResults.emit(results);


    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['searchType']) {
      const emptyQuery = {
        keywords: '',
        location: ''
      };

      this.searchForm.reset(emptyQuery); // ✅ vide le formulaire

      if (this.autoSearchOnTypeChange) {
        this.searchService.search(this.searchType, emptyQuery).subscribe(results => {
          this.searchMemory.setResults(this.searchType, results);
          this.searchResults.emit(results);
        });
      } else {
        // ✅ si autoSearch est désactivé, renvoyer ce qui est déjà en cache sans l’écraser
        const cached = this.searchMemory.getResults(this.searchType);
        this.searchResults.emit(cached);
      }
    }
  }



}
