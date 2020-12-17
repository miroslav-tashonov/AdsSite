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
      { path: '/home/left-sidebar/collection/Cars', title: 'Cars', image: 'assets/images/feature/cars.jpg', type: 'link' },
      { path: '/home/left-sidebar/collection/Apartments', title: 'Apartments', image: 'assets/images/feature/category-page(right).jpg', type: 'link' },
      { path: '/home/left-sidebar/collection/Diesel', title: 'Diesel', image: 'assets/images/feature/category-page(no-sidebar).jpg', type: 'link' },
      { path: '/home/left-sidebar/collection/Small', title: 'Small', image: 'assets/images/feature/category-page(no-sidebar).jpg', type: 'link' },
      { path: '/home/left-sidebar/collection/Large', title: 'Large', image: 'assets/images/feature/category-page(no-sidebar).jpg', type: 'link' }
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
