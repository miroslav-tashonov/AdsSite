import { Injectable } from '@angular/core';
import { Http, RequestMethod, RequestOptions } from '@angular/http';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../classes/product';
import { BehaviorSubject, Observable, of, Subscriber} from 'rxjs';
import { map, filter, scan } from 'rxjs/operators';
import 'rxjs/add/operator/map';
import { environment } from 'src/environments/environment';
import { HttpHeaders } from '@angular/common/http';

// Get product from Localstorage
let products = JSON.parse(localStorage.getItem("compareItem")) || [];

@Injectable()

export class ProductsService {
  
  public currency : string = 'USD';
  public catalogMode : boolean = false;
  
  public compareProducts : BehaviorSubject<Product[]> = new BehaviorSubject([]);
  public observer: Subscriber<{}>;
  public someProducts: BehaviorSubject<Product[]> = new BehaviorSubject([]);

  myAppUrl: string;
  myApiUrl: string;
  latestAdsApiUrl: string;
  relatedAdsApiUrl: string;
  adProductDetailsApiUrl: string;

  // Initialize 
  constructor(private http: Http, private toastrService: ToastrService) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/AdsApi/';
    this.latestAdsApiUrl = 'api/AdsApi/getLatestAds';
    this.adProductDetailsApiUrl = 'api/AdsApi/getProductDetails';
    this.relatedAdsApiUrl = 'api/AdsApi/getRelatedAds';

    this.compareProducts.subscribe(products => products = products);
  }

  // Observable Product Array
  private products(): Observable<Product[]> {
    return this.http.get(this.myAppUrl + this.myApiUrl).map((res: any) => res.json());
  }

  private latestProducts(): Observable<Product[]> {
    return this.http.get(this.myAppUrl + this.latestAdsApiUrl).map((res: any) => res.json())
  }

  public getLatestProducts(): Observable<Product[]> {
    return this.latestProducts();
  }

  private relatedProducts(): Observable<Product[]> {
    return this.http.get(this.myAppUrl + this.relatedAdsApiUrl).map((res: any) => res.json())
  }

  public getRelatedProducts(): Observable<Product[]> {
    return this.relatedProducts();
  }


  // Get Products
  public getProducts(): Observable<Product[]> {
    return this.products();
  }

  // Get Products By Id
  public getProduct(id: string): Observable<Product> {
    return this.http.post(this.myAppUrl + this.adProductDetailsApiUrl, { 'id': id }).map((res: any) => res.json())
  }

   // Get Products By category
  public getProductByCategory(category: string): Observable<Product[]> {
    return this.products().pipe(map(items => 
       items.filter((item: Product) => {
         if(category == 'all')
            return item
         else
            return item.category === category; 
        
       })
     ));
  }
  
   /*
      ---------------------------------------------
      ----------  Compare Product  ----------------
      ---------------------------------------------
   */

  // Get Compare Products
  public getComapreProducts(): Observable<Product[]> {
    const itemsStream = new Observable(observer => {
      observer.next(products);
      observer.complete();
    });
    return <Observable<Product[]>>itemsStream;
  }

  // If item is aleready added In compare
  public hasProduct(product: Product): boolean {
    const item = products.find(item => item.id === product.id);
    return item !== undefined;
  }

  // Add to compare
  public addToCompare(product: Product): Product | boolean {
    var item: Product | boolean = false;
    if (this.hasProduct(product)) {
      item = products.filter(item => item.id === product.id)[0];
      const index = products.indexOf(item);
    } else {
      if(products.length < 4)
        products.push(product);
      else 
        this.toastrService.warning('Maximum 4 products are in compare.'); // toasr services
    }
      localStorage.setItem("compareItem", JSON.stringify(products));
      return item;
  }

  // Removed Product
  public removeFromCompare(product: Product) {
    if (product === undefined) { return; }
    const index = products.indexOf(product);
    products.splice(index, 1);
    localStorage.setItem("compareItem", JSON.stringify(products));
  }
   
}
