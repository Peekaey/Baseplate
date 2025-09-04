<script lang="ts">
	import '../app.css';
	import { ModeWatcher } from "mode-watcher";
	import ThemeToggle from "$lib/components/layout/ThemeToggle.svelte";

	import favicon from '$lib/assets/favicon.svg';

	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<ModeWatcher />


<header class="header">
	<div class="container">
		<div class="nav-content">
			<div class="nav-left">
				<a href="/" class="logo">Baseplate</a>
			</div>
			<div class="nav-right">
				<ThemeToggle />
			</div>
		</div>
	</div>
</header>


<main class="main">
	{@render children?.()}
</main>

<style>
	.header {
		background: hsl(var(--background));
		border-bottom: 1px solid hsl(var(--border));
		position: sticky;
		top: 0;
		z-index: 50;
	}

	.container {
		max-width: 1200px;
		margin: 0 auto;
		padding: 0 1rem;
	}

	.nav-content {
		display: flex;
		align-items: center;
		justify-content: space-between;
		height: 4rem;
	}

	.nav-left {
		display: flex;
		align-items: center;
	}

	.logo {
		font-size: 1.25rem;
		font-weight: 600;
		text-decoration: none;
		color: hsl(var(--foreground));
	}

	.nav-right {
		display: flex;
		align-items: center;
	}

	.main {
		flex: 1;
		min-height: calc(100vh - 4rem);
	}

	
	:global(body) {
		margin: 0;
		display: flex;
		flex-direction: column;
		min-height: 100vh;
	}
</style>