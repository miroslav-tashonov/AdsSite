import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { ToastrModule } from 'ngx-toastr';
import { Observable, of, throwError } from 'rxjs';
import { CategoryViewModel } from '../models/CategoryViewModel';

import { CategoriesService } from './categories.service';
import { NotificationService } from './notification.service';

describe('CategoriesService', () => {
  let categoriesService: CategoriesService;
  let httpClientSpy: { get: jasmine.Spy };
  let notificationService: jasmine.SpyObj<NotificationService>;

  beforeEach(() => {
    notificationService = jasmine.createSpyObj('NotificationService', ['showError']);
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);

    TestBed.configureTestingModule({
      imports: [
        ToastrModule.forRoot()
      ],
      providers: [
        CategoriesService,
        { provide: NotificationService, useValue: notificationService },
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
    notificationService = TestBed.inject(NotificationService) as jasmine.SpyObj<NotificationService>;
    categoriesService = TestBed.inject(CategoriesService);

  });

  it('should be created', () => {
    expect(categoriesService).toBeTruthy();
  });


  it('should return stub value in getCategoriesTreeMenu', () => {
    const expectedCategories: CategoryViewModel[] =
      [{ id: '1', name: 'A' }, { id: '2', name: 'B' }];
    const countryId = '1';

    httpClientSpy.get.and.returnValue(of(expectedCategories));

    categoriesService.getCategoriesTreeMenu(countryId).subscribe(
      categories => expect(categories).toEqual(expectedCategories, 'expected heroes'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });

  it('should return an error when the server returns a 404', () => {
    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404, statusText: 'Not Found'
    });
    const countryId = '1';

    notificationService.showError.and.returnValue();
    httpClientSpy.get.and.returnValue(throwError(errorResponse));

    categoriesService.getCategoriesTreeMenu(countryId).subscribe(
      categories => fail('expected an error, not data'),
      error => categoriesService.errorHandler(error),
    );

    expect(notificationService.showError.calls.count()).toBe(1, 'one call');
  });

});
