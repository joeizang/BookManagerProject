import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/publishers')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/publishers"!</div>
}
