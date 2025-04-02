
export default function Footer() {
  return (
    <footer className="bg-gray-900 text-white py-4 fixed bottom-0 w-full">
      <div className="container mx-auto px-4 sm:px-6 static bottom-0 h-16">
        <div className="flex items-center justify-between">
          <div className="text-sm">&copy; 2025 ProdigeeNet Limited</div>
          <div className="flex space-x-4">
            <a href="#" className="text-gray-400 hover:text-white">F</a>
            <a href="#" className="text-gray-400 hover:text-white">X</a>
            <a href="#" className="text-gray-400 hover:text-white">I</a>
          </div>
        </div>
      </div>
    </footer>
  )
}