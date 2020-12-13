// Menu
export interface Menu {
  path?: string;
  title?: string;
  type?: string;
  megaMenu?: boolean;
  megaMenuType?: string; // small, medium, large
  image?: string;
  children?: Menu[];
}

export const MENUITEMS: Menu[] = [
  {
    title: 'home', type: 'link', path: 'home/one'
  },
  {
    title: 'categories', type: 'sub', megaMenu: true, megaMenuType: 'small', children: [
      { path: '/home/left-sidebar/collection/all', title: 'category-left-sidebar', image: 'assets/images/feature/category-page.jpg', type: 'link' },
      { path: '/home/right-sidebar/collection/all', title: 'category-right-sidebar', image: 'assets/images/feature/category-page(right).jpg', type: 'link' },
      { path: '/home/no-sidebar/collection/all', title: 'category-no-sidebar', image: 'assets/images/feature/category-page(no-sidebar).jpg', type: 'link' }
    ]
  },
  {
    title: 'Sub-categories', type: 'sub', megaMenu: true, megaMenuType: 'large', children: [
      {
        title: 'mens-fashion', type: 'link', children: [
          { path: '/home/left-sidebar/collection/all', title: 'sports-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'top', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'bottom', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'ethic-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'sports-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'shirts', type: 'link' }
        ]
      },
      {
        title: 'women-fashion', type: 'link', children: [
          { path: '/home/left-sidebar/collection/all', title: 'dresses', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'skirts', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'westarn-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'ethic-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'sports-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'bottom-wear', type: 'link' }
        ]
      },
      {
        title: 'kids-fashion', type: 'link', children: [
          { path: '/home/left-sidebar/collection/all', title: 'sports-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'ethic-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'sports-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'top', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'bottom-wear', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'ethic-wear', type: 'link' }
        ]
      },
      {
        title: 'accessories', type: 'link', children: [
          { path: '/home/left-sidebar/collection/all', title: 'fashion-jewellery', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'caps-and-hats', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'precious-jewellery', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'necklaces', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'earrings', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'rings-wrist-wear', type: 'link' }
        ]
      },
      {
        title: 'men-accessories', type: 'link', children: [
          { path: '/home/left-sidebar/collection/all', title: 'ties', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'cufflinks', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'pockets-squares', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'helmets', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'scarves', type: 'link' },
          { path: '/home/left-sidebar/collection/all', title: 'phone-cases', type: 'link' }
        ]
      },
    ]
  },
  /*
  {
    title: 'products', type: 'sub', megaMenu: true, megaMenuType: 'medium', children: [
        { path: '/home/left-sidebar/product/1', title: 'left-sidebar', image: 'assets/images/feature/product-page(left-sidebar).jpg', type: 'link' },
        { path: '/home/right-sidebar/product/1', title: 'right-sidebar', image: 'assets/images/feature/product-page(right-sidebar).jpg', type: 'link' },
        { path: '/home/no-sidebar/product/1', title: 'no-sidebar',  image: 'assets/images/feature/product-page(no-sidebar).jpg', type: 'link' },
        { path: '/home/col-left/product/1', title: '3-col-thumbnail-left', image: 'assets/images/feature/product-page(3-col-left).jpg', type: 'link' },
        { path: '/home/col-right/product/1', title: '3-col-thumbnail-right', image: 'assets/images/feature/product-page(3-col-right).jpg', type: 'link' },
        { path: '/home/column/product/1', title: 'thumbnail-below', image: 'assets/images/feature/product-page(3-column).jpg', type: 'link' },
        { path: '/home/accordian/product/1', title: 'accordian-details', image: 'assets/images/feature/product-page(accordian).jpg', type: 'link' },
        { path: '/home/left-image/product/1', title: 'thumbnail-left', image: 'assets/images/feature/product-page(left-image).jpg', type: 'link' },
        { path: '/home/right-image/product/1', title: 'thumbnail-right', image: 'assets/images/feature/product-page(right-image).jpg', type: 'link' },
        { path: '/home/vertical/product/1', title: 'vertical-tab', image: 'assets/images/feature/product-page(vertical-tab).jpg', type: 'link' }
      ]
  },*/
  {
    title: 'About us', type: 'link', path: '/pages/about-us'
  },
  {
    title: 'Contact', type: 'link', path: '/pages/contact'
  },
  {
    title: 'FAQ', type: 'link', path: '/pages/faq'
  },
]
