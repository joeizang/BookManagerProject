import { Link } from "@tanstack/react-router";
import { useState } from "react";

export default function Navbar() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <nav className="bg-gray-900 text-white">
      <div className="container mx-auto px-4 sm:px-6 py-4">
        <div className="flex items-center justify-between">
          <div className="text-xl font-bold text-indigo-400">🌊</div>
          
          {/* Mobile menu button */}
          <div className="md:hidden">
            <button 
              onClick={toggleMenu}
              className="text-gray-400 hover:text-white focus:outline-none"
            >
              {isMenuOpen ? (
                <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                </svg>
              ) : (
                <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
                </svg>
              )}
            </button>
          </div>

          {/* Desktop Navigation */}
          <div className="hidden md:flex items-center space-x-10">
            {/* Nav Items */}
            <ul className="flex space-x-6 text-sm font-medium">
              <li>
                <Link
                  to="/"
                  activeProps={{ className: "bg-gray-800 rounded-md px-3 py-2 text-white" }}
                  inactiveProps={{ className: "hover:text-gray-300" }}
                >
                  Dashboard
                </Link>
              </li>
              <li>
                <Link 
                  to="/authors"
                  activeProps={{ className: "bg-gray-800 rounded-md px-3 py-2 text-white" }}
                  inactiveProps={{ className: "hover:text-gray-300" }}
                >
                  Authors
                </Link>
              </li>
              <li>
                <Link 
                  to="/publishers"
                  activeProps={{ className: "bg-gray-800 rounded-md px-3 py-2 text-white" }}
                  inactiveProps={{ className: "hover:text-gray-300" }}
                >
                  Publishers
                </Link>
              </li>
            </ul>
          </div>

          {/* Right: Notifications and Avatar */}
          <div className="hidden md:flex items-center space-x-4">
            <button className="text-gray-400 hover:text-white">
              <svg
                className="h-5 w-5"
                fill="none"
                stroke="currentColor"
                strokeWidth="2"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
                />
              </svg>
            </button>
            <img
              className="h-8 w-8 rounded-full"
              src="https://i.pravatar.cc/40"
              alt="User avatar"
            />
          </div>
        </div>
        
        {/* Mobile Navigation Menu */}
        {isMenuOpen && (
          <div className="md:hidden mt-4">
            <ul className="flex flex-col space-y-2 text-sm font-medium">
              <li>
                <Link
                  to="/"
                  activeProps={{ className: "block px-2 py-1 rounded bg-gray-800" }}
                  inactiveProps={{ className: "block px-2 py-1 rounded hover:bg-gray-800" }}
                >
                  Dashboard
                </Link>
              </li>
              <li>
                <Link 
                  to="/authors" 
                  activeProps={{ className: "block px-2 py-1 rounded bg-gray-800" }}
                  inactiveProps={{ className: "block px-2 py-1 rounded hover:bg-gray-800" }}
                >
                  Authors
                </Link>
              </li>
              <li>
                <Link 
                  to="/publishers" 
                  activeProps={{ className: "block px-2 py-1 rounded bg-gray-800" }}
                  inactiveProps={{ className: "block px-2 py-1 rounded hover:bg-gray-800" }}
                >
                  Publishers
                </Link>
              </li>
              <li className="pt-4 flex items-center justify-between">
                <button className="text-gray-400 hover:text-white">
                  <svg
                    className="h-5 w-5"
                    fill="none"
                    stroke="currentColor"
                    strokeWidth="2"
                    viewBox="0 0 24 24"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
                    />
                  </svg>
                </button>
                <img
                  className="h-8 w-8 rounded-full"
                  src="https://i.pravatar.cc/40"
                  alt="User avatar"
                />
              </li>
            </ul>
          </div>
        )}
      </div>
    </nav>
  )
}