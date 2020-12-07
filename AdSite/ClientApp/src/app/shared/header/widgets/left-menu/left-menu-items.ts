// Menu
export interface Menu {
  path?: string;
  title?: string;
  type?: string;
  megaMenu?: boolean;
  children?: Menu[];
}

export const MENUITEMS: Menu[] = [
	{
		title: 'clothing', type: 'sub', megaMenu: true, children: [
	      { 
	      	title: 'mens fashion',  type: 'link', children: [
		      	{ path: '/home/left-sidebar/collection/all', title: 'sports wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'top',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'bottom',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'ethic wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'sports wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'shirts',  type: 'link' },
		        { path: '/home/left-sidebar/collection/all', title: 'bottom',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'ethic wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'sports wear',  type: 'link' }
	      	]
	      },
	      { 
	      	title: 'women fashion',  type: 'link', children: [
		      	{ path: '/home/left-sidebar/collection/all', title: 'dresses',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'skirts',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'westarn wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'ethic wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'bottom',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'ethic wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'sports wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'sports wear',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'bottom wear',  type: 'link' }
	      	]
	      },
	    ]
	},
	{
		title: 'bags', type: 'sub', children: [
		  { path: '/home/left-sidebar/collection/all', title: 'shopper bags', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'laptop bags', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'clutches', type: 'link' },
		  { 
		  	path: '/home/left-sidebar/collection/all', title: 'purses', type: 'link', children: [
		      	{ path: '/home/left-sidebar/collection/all', title: 'purses',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'wallets',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'leathers',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'satchels',  type: 'link' }
	      	]
		  },
	    ]
	},
	{
		title: 'footwear', type: 'sub', children: [
		  { path: '/home/left-sidebar/collection/all', title: 'sport shoes', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'formal shoes', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'casual shoes', type: 'link' }
		]
	},
	{
		path: '/home/left-sidebar/collection/all', title: 'watches', type: 'link'
	},
	{
		title: 'Accessories', type: 'sub', children: [
		  { path: '/home/left-sidebar/collection/all', title: 'fashion jewellery', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'caps and hats', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'precious jewellery', type: 'link' },
		  { 
		  	path: '/home/left-sidebar/collection/all', title: 'more..', type: 'link', children: [
		      	{ path: '/home/left-sidebar/collection/all', title: 'necklaces',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'earrings',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'rings & wrist wear',  type: 'link' },
		      	{ 
		      		path: '/home/left-sidebar/collection/all', title: 'more...',  type: 'link', children: [
				      	{ path: '/home/left-sidebar/collection/all', title: 'ties',  type: 'link' },
				      	{ path: '/home/left-sidebar/collection/all', title: 'cufflinks',  type: 'link' },
				      	{ path: '/home/left-sidebar/collection/all', title: 'pockets squares',  type: 'link' },
				      	{ path: '/home/left-sidebar/collection/all', title: 'helmets',  type: 'link' },
				      	{ path: '/home/left-sidebar/collection/all', title: 'scarves',  type: 'link' },
				      	{ 
				      		path: '/home/left-sidebar/collection/all', title: 'more...',  type: 'link', children: [
						      	{ path: '/home/left-sidebar/collection/all', title: 'accessory gift sets',  type: 'link' },
						      	{ path: '/home/left-sidebar/collection/all', title: 'travel accessories',  type: 'link' },
						      	{ path: '/home/left-sidebar/collection/all', title: 'phone cases',  type: 'link' }
				      		]
				      	},
				    ]
		      	}
	      	]
		  },
	    ]
	},
	{
		path: '/home/left-sidebar/collection/all', title: 'house of design', type: 'link'
	},
	{
		title: 'beauty & personal care', type: 'sub', children: [
		  { path: '/home/left-sidebar/collection/all', title: 'makeup', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'skincare', type: 'link' },
		  { path: '/home/left-sidebar/collection/all', title: 'premium beaty', type: 'link' },
		  { 
		  	path: '/home/left-sidebar/collection/all', title: 'more..', type: 'link', children: [
		      	{ path: '/home/left-sidebar/collection/all', title: 'fragrances',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'luxury beauty',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'hair care',  type: 'link' },
		      	{ path: '/home/left-sidebar/collection/all', title: 'tools & brushes',  type: 'link' }
	      	]
		  },
	    ]
	},
	{
		path: '/home/left-sidebar/collection/all', title: 'home & decor', type: 'link'
	},
	{
		path: '/home/left-sidebar/collection/all', title: 'kitchen', type: 'link'
	},
]